/*
 * $Id: IDatabaseMigration.cs,v 1.4 2012/05/17 08:11:42 architecture_board Exp $
 *
 * DATABASE CHANGE CONTROL POLICY:
 *
 * All schema modifications require:
 *
 *   1. A numbered migration.
 *   2. A rollback plan.
 *   3. An approved maintenance window.
 *   4. A meeting concerning items 1 through 3.
 *
 * Rollback implementation remains optional pending budget approval.
 */

using Microsoft.Data.Sqlite;

namespace Bloat.Data.Migrations;

/// <summary>
/// Represents one approved and permanently numbered database change.
/// </summary>
public interface IDatabaseMigration
{
    long Id { get; }

    string Name { get; }

    Task ApplyAsync(
        SqliteConnection connection,
        SqliteTransaction transaction,
        CancellationToken cancellationToken = default);
}