/*
 * $Id: Migration0001CreateAmplificationCases.cs,v 1.0 2012/05/18 03:42:01 db_change_committee Exp $
 *
 * CHANGE REQUEST: BLOAT-DB-CR-0001
 *
 * PURPOSE:
 * Establish the initial external-resource amplification case registry.
 *
 * ROLLBACK PROCEDURE:
 * Do not.
 *
 * EXISTING INSTALLATIONS:
 * CREATE TABLE IF NOT EXISTS is used to adopt databases produced before
 * formal migration governance was invented.
 */

using Microsoft.Data.Sqlite;

namespace Bloat.Data.Migrations.Definitions;

public sealed class Migration0001CreateAmplificationCases : IDatabaseMigration
{
    public long Id => 1;

    public string Name => "0001_CreateAmplificationCases";

    public async Task ApplyAsync(
        SqliteConnection connection,
        SqliteTransaction transaction,
        CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(connection);
        ArgumentNullException.ThrowIfNull(transaction);

        using var command = connection.CreateCommand();

        command.Transaction = transaction;
        command.CommandText =
            """
            CREATE TABLE IF NOT EXISTS AmplificationCases
            (
                Token                     TEXT    NOT NULL PRIMARY KEY,
                CaseNumber                TEXT    NOT NULL UNIQUE,
                OriginalUrl               TEXT    NOT NULL,
                AmplifiedRelativeUrl      TEXT    NOT NULL,
                CreatedAtUnixMilliseconds INTEGER NOT NULL
            );
            """;

        await command.ExecuteNonQueryAsync(cancellationToken);
    }
}