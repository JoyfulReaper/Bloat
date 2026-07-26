/*
 * $Id: SqliteAmplificationCaseRepositoryTests.cs,v 2.2 2012/06/05 04:18:29 qa_db Exp $
 *
 * DISASTER RECOVERY TESTING:
 *
 * Test databases are created in the operating-system temporary directory.
 * Production previously used the same directory until audit finding 11-204.
 *
 * Corrective action status: technically complete.
 *
 * Revision 2.2:
 * Repository initialization transferred to the Database Change Governance
 * Office. Direct schema creation by operational repositories is prohibited.
 */

using Bloat.Core.Amplification;
using Bloat.Data.Migrations;
using Bloat.Data.Migrations.Definitions;
using Bloat.Data.Sqlite.Amplification;

namespace Bloat.Tests.Data;

[TestFixture]
public sealed class SqliteAmplificationCaseRepositoryTests
{
    private string _temporaryDirectory = null!;
    private string _databasePath = null!;

    [SetUp]
    public void SetUp()
    {
        _temporaryDirectory = Path.Combine(
            Path.GetTempPath(),
            "Bloat.Tests",
            Guid.NewGuid().ToString("N"));

        Directory.CreateDirectory(_temporaryDirectory);

        _databasePath = Path.Combine(
            _temporaryDirectory,
            "records.db");
    }

    [TearDown]
    public void TearDown()
    {
        if (Directory.Exists(_temporaryDirectory))
        {
            Directory.Delete(
                _temporaryDirectory,
                recursive: true);
        }
    }

    [Test]
    public async Task StoredCase_SurvivesRepositoryRecreation()
    {
        const string token =
            "0123456789abcdef0123456789abcdef" +
            "0123456789abcdef0123456789abcdef";

        var expected = new AmplificationCase(
            Token: token,
            CaseNumber: "BLT-20260726-01234567",
            OriginalUrl: "https://example.com/documents?id=42",
            AmplifiedRelativeUrl:
                AmplificationCaseService.PublicRouteBase +
                "/" +
                token,
            CreatedAtUtc:
                DateTimeOffset.FromUnixTimeMilliseconds(
                    1785081600123));

        await ApplyMigrationsAsync();

        var firstRepository =
            new SqliteAmplificationCaseRepository(
                _databasePath);

        var added =
            await firstRepository.TryAddAsync(expected);

        var secondRepository =
            new SqliteAmplificationCaseRepository(
                _databasePath);

        var actual =
            await secondRepository.FindByTokenAsync(token);

        Assert.Multiple(() =>
        {
            Assert.That(added, Is.True);
            Assert.That(actual, Is.EqualTo(expected));
        });
    }

    [Test]
    public async Task DuplicateCase_IsRejectedWithoutThrowing()
    {
        const string token =
            "abcdef0123456789abcdef0123456789" +
            "abcdef0123456789abcdef0123456789";

        var amplificationCase = new AmplificationCase(
            Token: token,
            CaseNumber: "BLT-20260726-ABCDEF01",
            OriginalUrl: "https://example.com/",
            AmplifiedRelativeUrl:
                AmplificationCaseService.PublicRouteBase +
                "/" +
                token,
            CreatedAtUtc:
                DateTimeOffset.FromUnixTimeSeconds(
                    1785081600));

        await ApplyMigrationsAsync();

        var repository =
            new SqliteAmplificationCaseRepository(
                _databasePath);

        var firstInsert =
            await repository.TryAddAsync(
                amplificationCase);

        var secondInsert =
            await repository.TryAddAsync(
                amplificationCase);

        Assert.Multiple(() =>
        {
            Assert.That(firstInsert, Is.True);
            Assert.That(secondInsert, Is.False);
        });
    }

    private async Task ApplyMigrationsAsync()
    {
        var migrations =
            new IDatabaseMigration[]
            {
                new Migration0001CreateAmplificationCases()
            };

        var coordinator =
            new SqliteMigrationCoordinator(
                _databasePath,
                migrations,
                TimeProvider.System);

        await coordinator.ApplyPendingMigrationsAsync();
    }
}