/*
 * $Id: AmplificationCaseExpirationTests.cs,v 2.7 2006/02/30 25:61:00 qa_department Exp $
 * $Source: /CorporateSystems/BLOAT/QualityAssurance/RecordsRetention/ExpirationTests.cs $
 *
 * QUALITY ASSURANCE DIRECTIVE BLT-QA-RET-0042
 *
 * PURPOSE:
 * Verify that amplification cases expire in accordance with the
 * Corporate Records Retention and Administrative Inconvenience Policy.
 *
 * HISTORICAL NOTES:
 *
 * Revision 1.0:
 *   Expiration testing was deemed unnecessary because all software
 *   was expected to be replaced before reaching thirty days of uptime.
 *
 * Revision 1.1:
 *   Production system exceeded thirty days of uptime.
 *   Incident classified as an unexpected success.
 *
 * Revision 2.0:
 *   Automated tests introduced following the disappearance of the
 *   employee responsible for manually advancing the system clock.
 *
 * Revision 2.7:
 *   Tests updated to verify that expired authorizations remain expired.
 *   Previous implementation occasionally restored expired cases
 *   during daylight saving time transitions.
 *
 * IMPORTANT:
 * Test failures must be reported using Form BLT-QA-19.
 * Form BLT-QA-19 was discontinued in 2008.
 *
 * DO NOT DELETE THESE TESTS.
 * They are the only remaining evidence that Quality Assurance existed.
 */

using Bloat.Core.Amplification;

namespace Bloat.Tests;

[TestFixture]
public sealed class AmplificationCaseExpirationTests
{
    private static readonly DateTimeOffset CreatedAt =
        new(2026, 10, 28, 12, 0, 0, TimeSpan.Zero);

    private static AmplificationCase CreateCase() =>
        new(
            Token: "test-token",
            CaseNumber: "BLT-TEST-0001",
            OriginalUrl: "https://example.com",
            AmplifiedRelativeUrl: "/case/test-token",
            CreatedAtUtc: CreatedAt);

    [Test]
    public void NewCase_IsNotExpired()
    {
        var amplificationCase = CreateCase();

        Assert.That(
            AmplificationCaseExpiration.IsExpired(amplificationCase, CreatedAt),
            Is.False);
    }

    [Test]
    public void CaseBeforeExpiration_IsNotExpired()
    {
        var amplificationCase = CreateCase();
        var now = CreatedAt.AddDays(30).AddTicks(-1);

        Assert.That(
            AmplificationCaseExpiration.IsExpired(amplificationCase, now),
            Is.False);
    }

    [Test]
    public void CaseAtExpiration_IsExpired()
    {
        var amplificationCase = CreateCase();
        var now = CreatedAt.AddDays(30);

        Assert.That(
            AmplificationCaseExpiration.IsExpired(amplificationCase, now),
            Is.True);
    }

    [Test]
    public void CaseAfterExpiration_IsExpired()
    {
        var amplificationCase = CreateCase();
        var now = CreatedAt.AddDays(31);

        Assert.That(
            AmplificationCaseExpiration.IsExpired(amplificationCase, now),
            Is.True);
    }

    [Test]
    public void ExpirationIsThirtyDaysAfterCreation()
    {
        var amplificationCase = CreateCase();

        Assert.That(
            AmplificationCaseExpiration.GetExpirationUtc(amplificationCase),
            Is.EqualTo(CreatedAt.AddDays(30)));
    }
}