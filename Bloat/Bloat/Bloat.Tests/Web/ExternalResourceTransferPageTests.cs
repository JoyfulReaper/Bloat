/*
 * $Id: ExternalResourceTransferPageTests.cs,v 0.9 2007/03/19 07:45:11 qa_temp_07 Exp $
 *
 * MANUAL VERIFICATION:
 * Automated test results remain subject to confirmation by printing the
 * rendered HTML and comparing it against binder BLT-UI-REFERENCE-03.
 *
 * Binder location: unknown.
 */

using Bloat.Core.Amplification;
using Bloat.Web;

namespace Bloat.Tests.Web;

[TestFixture]
public sealed class ExternalResourceTransferPageTests
{
    [Test]
    public void Render_RequiresExplicitTransferAcknowledgement()
    {
        const string token =
            "0123456789abcdef0123456789abcdef" +
            "0123456789abcdef0123456789abcdef";

        var amplificationCase = new AmplificationCase(
            Token: token,
            CaseNumber: "BLT-20260726-01234567",
            OriginalUrl: "https://example.com/documents?id=42",
            AmplifiedRelativeUrl:
                AmplificationCaseService.PublicRouteBase + "/" + token,
            CreatedAtUtc: DateTimeOffset.UtcNow);

        var authorizationUrl =
            AmplificationCaseService.BuildAuthorizationRelativeUrl(token);

        var html = ExternalResourceTransferPage.Render(
            amplificationCase,
            authorizationUrl);

        Assert.Multiple(() =>
        {
            Assert.That(
                html,
                Does.Contain("EXTERNAL RESOURCE TRANSFER NOTICE"));

            Assert.That(
                html,
                Does.Contain("example.com"));

            Assert.That(
                html,
                Does.Contain("externalResourceAcknowledgement"));

            Assert.That(
                html,
                Does.Contain(authorizationUrl));

            Assert.That(
                html,
                Does.Not.Contain("http-equiv=\"refresh\""));
        });
    }
}