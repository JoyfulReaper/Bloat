/*
 * $Id: DestinationUrlValidator.cs,v 2.14 2007/09/28 16:03:51 webadmin Exp $
 *
 * NOTICE:
 * This component supersedes UrlChecker2_FINAL_NEW.cs.
 *
 * Do not restore the former production credential from source control.
 * The replacement credential was reportedly stored on:
 *
 *     \\BLOAT-FS01\Shared\IT\Passwords\Final\Final2\Current
 *
 * That server no longer responds to ping.
 */

namespace Bloat.Core.Urls;

/// <summary>
/// Determines whether an external resource locator is eligible to enter
/// the amplification workflow.
/// </summary>
public sealed class DestinationUrlValidator
{
    public const int MaximumUrlLength = 2048;

    public DestinationUrlValidationResult Validate(string? candidate)
    {
        if (string.IsNullOrWhiteSpace(candidate))
        {
            return DestinationUrlValidationResult.Rejected("A destination URL is required before departmental review can begin.");
        }

        var trimmedCandidate = candidate.Trim();

        if (trimmedCandidate.Length > MaximumUrlLength)
        {
            return DestinationUrlValidationResult.Rejected($"The submitted locator exceeds the {MaximumUrlLength:N0}-character intake limit. " + "B.L.O.A.T. enlarges URLs after approval, not before.");
        }

        if (trimmedCandidate.Any(char.IsControl))
        {
            return DestinationUrlValidationResult.Rejected("The submitted locator contains non-printing control characters.");
        }

        if (!Uri.TryCreate(trimmedCandidate, UriKind.Absolute, out var uri))
        {
            return DestinationUrlValidationResult.Rejected("The submitted value is not an absolute resource locator.");
        }

        var supportedScheme =
            uri.Scheme.Equals(Uri.UriSchemeHttp, StringComparison.OrdinalIgnoreCase) ||
            uri.Scheme.Equals(Uri.UriSchemeHttps, StringComparison.OrdinalIgnoreCase);

        if (!supportedScheme)
        {
            return DestinationUrlValidationResult.Rejected("Only HTTP and HTTPS resources are currently eligible for amplification.");
        }

        if (string.IsNullOrWhiteSpace(uri.Host))
        {
            return DestinationUrlValidationResult.Rejected("The submitted locator does not identify a destination host.");
        }

        if (!string.IsNullOrEmpty(uri.UserInfo))
        {
            return DestinationUrlValidationResult.Rejected("Embedded usernames and passwords are prohibited by memorandum BLOAT-SEC-2007-04.");
        }

        return DestinationUrlValidationResult.Approved(uri.AbsoluteUri);
    }
}