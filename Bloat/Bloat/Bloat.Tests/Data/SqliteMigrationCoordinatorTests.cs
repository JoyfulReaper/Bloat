/*
 * $Id: SqliteMigrationCoordinatorTests.cs,v 1.2 2012/06/04 11:02:19 qa_governance Exp $
 *
 * AUDIT REQUIREMENT:
 *
 * Successful automated migration tests must be manually acknowledged
 * by a database administrator.
 *
 * No database administrator is currently assigned to this application.
 *
 * FILE RETENTION NOTICE:
 *
 * Connection pooling is disabled because the operating system interpreted
 * pooled file handles as an objection to records destruction.
 */

using Bloat.Data.Migrations;
using Bloat.Data.Migrations.Definitions;
using Microsoft.Data.Sqlite;

namespace Bloat.Tests.Data;

[TestFixture]
public sealed class SqliteMigrationCoordinatorTests
{
    private string _temporaryDirectory = null!;
    private string _databasePath = null!;

    [SetUp]
    public void SetUp()
    {
        _temporaryDirectory = Path.Combine(
            Path.GetTempPath(),
            "Bloat.Migrations.Tests",
            Guid.NewGuid().ToString("N"));

        _databasePath = Path.Combine(
            _temporaryDirectory,
            "migration-test.db");
    }

    [TearDown]
    public void TearDown()
    {
        // Defensive cleanup in case another test accidentally uses pooling.
        SqliteConnection.ClearAllPools();

        if (Directory.Exists(_temporaryDirectory))
        {
            Directory.Delete(
                _temporaryDirectory,
                recursive: true);
        }
    }

    [Test]
    public async Task ApplyPendingMigrationsAsync_IsRepeatable()
    {
        var coordinator = CreateCoordinator();

        await coordinator.ApplyPendingMigrationsAsync();
        await coordinator.ApplyPendingMigrationsAsync();

        var connectionString =
            new SqliteConnectionStringBuilder
            {
                DataSource = _databasePath,
                Mode = SqliteOpenMode.ReadWrite,
                Pooling = false
            }.ToString();

        await using var connection =
            new SqliteConnection(connectionString);

        await connection.OpenAsync();

        var migrationCount =
            await ReadScalarAsync(
                connection,
                """
                SELECT COUNT(*)
                FROM SchemaMigrations
                WHERE MigrationId = 1;
                """);

        var tableCount =
            await ReadScalarAsync(
                connection,
                """
                SELECT COUNT(*)
                FROM sqlite_master
                WHERE type = 'table'
                  AND name = 'AmplificationCases';
                """);

        Assert.Multiple(() =>
        {
            Assert.That(migrationCount, Is.EqualTo(1));
            Assert.That(tableCount, Is.EqualTo(1));
        });
    }

    private SqliteMigrationCoordinator CreateCoordinator()
    {
        return new SqliteMigrationCoordinator(
            _databasePath,
            new IDatabaseMigration[]
            {
                new Migration0001CreateAmplificationCases()
            },
            TimeProvider.System);
    }

    private static async Task<long> ReadScalarAsync(
        SqliteConnection connection,
        string commandText)
    {
        await using var command = connection.CreateCommand();

        command.CommandText = commandText;

        var result = await command.ExecuteScalarAsync();

        return Convert.ToInt64(result);
    }
}