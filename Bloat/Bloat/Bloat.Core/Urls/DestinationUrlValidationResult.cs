/*
 * $Id: DestinationUrlValidationResult.cs,v 1.3 2006/04/12 08:42:17 svc_build Exp $
 * $Source: /CorporateSystems/BLOAT/Core/Validation/DestinationUrlValidationResult.cs $
 *
 * DATA CLASSIFICATION: INTERNAL / FORM PROCESSING
 *
 * Historical note:
 * The original validation result was represented by the integers
 * 0, 1, 4, 7, and -12. Their meanings could not be recovered.
 */

namespace Bloat.Core.Urls;

public sealed record DestinationUrlValidationResult(
    bool IsValid,
    string? NormalizedUrl,
    string? FailureReason)
{
    public static DestinationUrlValidationResult Approved(string normalizedUrl)
    {
        return new DestinationUrlValidationResult(
            IsValid: true,
            NormalizedUrl: normalizedUrl,
            FailureReason: null);
    }

    public static DestinationUrlValidationResult Rejected(string failureReason)
    {
        return new DestinationUrlValidationResult(
            IsValid: false,
            NormalizedUrl: null,
            FailureReason: failureReason);
    }
}