/*
 * $Id: DestinationUrlValidatorTests.cs,v 1.6 2007/10/01 07:12:04 qa_temp_03 Exp $
 *
 * TEST DATA CLASSIFICATION: SYNTHETIC
 *
 * The QA database password previously included in this file was
 * determined to be the production database password.
 *
 * Remediation status: MEETING SCHEDULED
 */

using Bloat.Core.Urls;

namespace Bloat.Tests.Core.Urls;

[TestFixture]
public sealed class DestinationUrlValidatorTests
{
    private DestinationUrlValidator _validator = null!;

    [SetUp]
    public void SetUp()
    {
        _validator = new DestinationUrlValidator();
    }

    [TestCase(
        "https://example.com",
        "https://example.com/")]
    [TestCase(
        "http://example.com/documents?id=42",
        "http://example.com/documents?id=42")]
    public void Validate_ApprovesSupportedAbsoluteUrls(
        string candidate,
        string expectedNormalizedUrl)
    {
        var result = _validator.Validate(candidate);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.True);
            Assert.That(
                result.NormalizedUrl,
                Is.EqualTo(expectedNormalizedUrl));
            Assert.That(result.FailureReason, Is.Null);
        });
    }

    [TestCase(null)]
    [TestCase("")]
    [TestCase("   ")]
    [TestCase("/relative/resource")]
    [TestCase("ftp://example.com/archive.zip")]
    [TestCase("C:\\Documents\\secret-url.txt")]
    public void Validate_RejectsIneligibleValues(string? candidate)
    {
        var result = _validator.Validate(candidate);

        Assert.Multiple(() =>
        {
            Assert.That(result.IsValid, Is.False);
            Assert.That(result.NormalizedUrl, Is.Null);
            Assert.That(result.FailureReason, Is.Not.Empty);
        });
    }

    [Test]
    public void Validate_RejectsEmbeddedCredentials()
    {
        const string candidate =
            "https://administrator:DefinitelyNotThePassword@example.com/payroll";

        var result = _validator.Validate(candidate);

        Assert.That(result.IsValid, Is.False);
        Assert.That(
            result.FailureReason,
            Does.Contain("Embedded usernames and passwords"));
    }

    [Test]
    public void Validate_RejectsUrlsExceedingTheIntakeLimit()
    {
        var candidate = new string(
            'x',
            DestinationUrlValidator.MaximumUrlLength + 1);

        var result = _validator.Validate(candidate);

        Assert.That(result.IsValid, Is.False);
        Assert.That(
            result.FailureReason,
            Does.Contain("intake limit"));
    }
}