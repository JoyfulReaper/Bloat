/*
 * $Id: SqliteMigrationCoordinator.cs,v 3.7 2012/06/01 01:19:54 release_management Exp $
 *
 * OPERATIONAL NOTICE:
 *
 * This coordinator replaced a Word document named:
 *
 *     Database Changes FINAL v7 USE THIS ONE.doc
 *
 * The document remains the authoritative source according to policy,
 * but could not be opened after Office 2003 was decommissioned.
 *
 * The database is therefore maintained from this non-authoritative code.
 */

using Microsoft.Data.Sqlite;

namespace Bloat.Data.Migrations;

/// <summary>
/// Applies approved SQLite schema migrations in numerical order and
/// records their completion in the schema migration ledger.
/// </summary>
public sealed class SqliteMigrationCoordinator
{
    private readonly string _databasePath;
    private readonly string _connectionString;
    private readonly IReadOnlyList<IDatabaseMigration> _migrations;
    private readonly TimeProvider _timeProvider;

    public SqliteMigrationCoordinator(
        string databasePath,
        IEnumerable<IDatabaseMigration> migrations,
        TimeProvider timeProvider)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(databasePath);
        ArgumentNullException.ThrowIfNull(migrations);
        ArgumentNullException.ThrowIfNull(timeProvider);

        _databasePath = Path.GetFullPath(databasePath);
        _timeProvider = timeProvider;

        _migrations = migrations
            .OrderBy(migration => migration.Id)
            .ToArray();

        ValidateMigrationCatalog(_migrations);

        var connectionStringBuilder =
            new SqliteConnectionStringBuilder
            {
                DataSource = _databasePath,
                Mode = SqliteOpenMode.ReadWriteCreate,
                Cache = SqliteCacheMode.Shared,
                Pooling = false
            };

        _connectionString = connectionStringBuilder.ToString();
    }

    public async Task ApplyPendingMigrationsAsync(
        CancellationToken cancellationToken = default)
    {
        EnsureDatabaseDirectoryExists();

        using var connection = new SqliteConnection(_connectionString);
        await connection.OpenAsync(cancellationToken);
        await EnsureMigrationLedgerExistsAsync(connection, cancellationToken);

        var appliedMigrationIds = await ReadAppliedMigrationIdsAsync(connection, cancellationToken);

        EnsureDatabaseIsNotAheadOfApplication(appliedMigrationIds);

        foreach (var migration in _migrations)
        {
            if (appliedMigrationIds.Contains(migration.Id))
            {
                continue;
            }
            using var transaction = connection.BeginTransaction();

            try
            {
                await migration.ApplyAsync(
                    connection,
                    transaction,
                    cancellationToken);

                await RecordAppliedMigrationAsync(
                    connection,
                    transaction,
                    migration,
                    cancellationToken);

                transaction.Commit();
            }
            catch
            {
                transaction.Rollback();
                throw;
            }
        }
    }

    private void EnsureDatabaseDirectoryExists()
    {
        var databaseDirectory = Path.GetDirectoryName(_databasePath);
        if (!string.IsNullOrWhiteSpace(databaseDirectory))
        {
            Directory.CreateDirectory(databaseDirectory);
        }
    }

    private static async Task EnsureMigrationLedgerExistsAsync(
        SqliteConnection connection,
        CancellationToken cancellationToken)
    {
        using var command = connection.CreateCommand();

        command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS SchemaMigrations
            (
                MigrationId              INTEGER NOT NULL PRIMARY KEY,
                MigrationName            TEXT    NOT NULL,
                AppliedAtUnixMilliseconds INTEGER NOT NULL
            );
            """;

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static async Task<HashSet<long>>
        ReadAppliedMigrationIdsAsync(SqliteConnection connection, CancellationToken cancellationToken)
    {
        using var command = connection.CreateCommand();

        command.CommandText =
            """
            SELECT MigrationId
            FROM SchemaMigrations
            ORDER BY MigrationId;
            """;

        using var reader = await command.ExecuteReaderAsync(cancellationToken);
        var migrationIds = new HashSet<long>();

        while (await reader.ReadAsync(cancellationToken))
        {
            migrationIds.Add(reader.GetInt64(0));
        }

        return migrationIds;
    }

    private async Task RecordAppliedMigrationAsync(
        SqliteConnection connection,
        SqliteTransaction transaction,
        IDatabaseMigration migration,
        CancellationToken cancellationToken)
    {
        using var command = connection.CreateCommand();
        command.Transaction = transaction;

        command.CommandText =
            """
            INSERT INTO SchemaMigrations
            (
                MigrationId,
                MigrationName,
                AppliedAtUnixMilliseconds
            )
            VALUES
            (
                $migrationId,
                $migrationName,
                $appliedAtUnixMilliseconds
            );
            """;

        command.Parameters.AddWithValue("$migrationId", migration.Id);
        command.Parameters.AddWithValue("$migrationName", migration.Name);

        command.Parameters.AddWithValue("$appliedAtUnixMilliseconds",
            _timeProvider.GetUtcNow()
                .ToUnixTimeMilliseconds());

        await command.ExecuteNonQueryAsync(cancellationToken);
    }

    private static void ValidateMigrationCatalog(
        IReadOnlyCollection<IDatabaseMigration> migrations)
    {
        foreach (var migration in migrations)
        {
            if (migration.Id <= 0)
            {
                throw new InvalidOperationException($"Migration '{migration.Name}' has an invalid ID.");
            }

            if (string.IsNullOrWhiteSpace(migration.Name))
            {
                throw new InvalidOperationException($"Migration {migration.Id} does not have a name.");
            }
        }

        var duplicateId = migrations
            .GroupBy(migration => migration.Id)
            .FirstOrDefault(group => group.Count() > 1);

        if (duplicateId is not null)
        {
            throw new InvalidOperationException(
                $"Multiple migrations use ID {duplicateId.Key}.");
        }
    }

    private void EnsureDatabaseIsNotAheadOfApplication(
        IEnumerable<long> appliedMigrationIds)
    {
        var knownMigrationIds = _migrations
            .Select(migration => migration.Id)
            .ToHashSet();

        var unknownMigrationIds = appliedMigrationIds
            .Where(id => !knownMigrationIds.Contains(id))
            .OrderBy(id => id)
            .ToArray();

        if (unknownMigrationIds.Length == 0)
        {
            return;
        }

        throw new InvalidOperationException(
            "The database contains migration records unknown to this " +
            "application build: " +
            string.Join(", ", unknownMigrationIds) +
            ". Downgrade authorization was not located.");
    }
}