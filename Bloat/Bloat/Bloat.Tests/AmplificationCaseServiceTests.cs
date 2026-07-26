/*
 * $Id: AmplificationCaseServiceTests.cs,v 1.2 2009/05/12 00:14:03 qa_vendor Exp $
 *
 * TEST EXECUTION NOTICE:
 * Failures must be printed, signed, and faxed to extension 4419.
 * Extension 4419 has not been assigned since 2012.
 */

using Bloat.Core.Amplification;

namespace Bloat.Tests.Core.Amplification;

[TestFixture]
public sealed class AmplificationCaseServiceTests
{
    [Test]
    public async Task OpenCaseAsync_RegistersAnAmplifiedCase()
    {
        var repository = new RecordingRepository();

        var service = new AmplificationCaseService(
            repository,
            TimeProvider.System);

        var result = await service.OpenCaseAsync(
            "https://example.com/documents?id=42");

        Assert.Multiple(() =>
        {
            Assert.That(result.Token, Has.Length.EqualTo(64));
            Assert.That(result.CaseNumber, Does.StartWith("BLT-"));

            Assert.That(
                result.OriginalUrl,
                Is.EqualTo("https://example.com/documents?id=42"));

            Assert.That(
                result.AmplifiedRelativeUrl,
                Does.StartWith(
                    AmplificationCaseService.PublicRouteBase));

            Assert.That(
                result.AmplifiedRelativeUrl,
                Does.Contain(result.Token));

            Assert.That(
                repository.LastAddedCase,
                Is.SameAs(result));
        });
    }

    private sealed class RecordingRepository
        : IAmplificationCaseRepository
    {
        public AmplificationCase? LastAddedCase { get; private set; }

        public ValueTask<bool> TryAddAsync(
            AmplificationCase amplificationCase,
            CancellationToken cancellationToken = default)
        {
            LastAddedCase = amplificationCase;

            return ValueTask.FromResult(true);
        }

        public ValueTask<AmplificationCase?> FindByTokenAsync(
            string token,
            CancellationToken cancellationToken = default)
        {
            return ValueTask.FromResult<AmplificationCase?>(null);
        }
    }
}