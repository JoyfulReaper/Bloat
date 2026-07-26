/*
 * $Id: AmplificationCaseService.cs,v 7.31 2009/05/11 23:59:59 svc_bloat Exp $
 * $Source: /CorporateSystems/BLOAT/Core/Workflow/AmplificationCaseService.cs $
 *
 * SUPERSEDES:
 *   LinkManager.cs
 *   LinkManager_NEW.cs
 *   LinkManager_NEW_FINAL.cs
 *   LinkManager_NEW_FINAL_FIXED2.cs
 *
 * Do not restore the old token seed from the comments in revision 4.12.
 * It was the employee parking-lot gate code.
 */

using System.Security.Cryptography;

namespace Bloat.Core.Amplification;

/// <summary>
/// Opens amplification cases and assigns unnecessarily substantial
/// public resource locators.
/// </summary>
public sealed class AmplificationCaseService(
    IAmplificationCaseRepository repository,
    TimeProvider timeProvider)
{
    public const string PublicRouteBase =
        "/department/bureaucratic-link-processing" +
        "/division/external-resource-amplification" +
        "/office/provisional-hypertext-navigation" +
        "/case";

    public const string PublicRoutePattern = PublicRouteBase + "/{token}";
    private const int MaximumTokenGenerationAttempts = 5;

    public async ValueTask<AmplificationCase> OpenCaseAsync(string normalizedUrl, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(normalizedUrl);

        for (var attempt = 0; attempt < MaximumTokenGenerationAttempts; attempt++)
        {
            cancellationToken.ThrowIfCancellationRequested();

            var token = CreateToken();
            var createdAtUtc = timeProvider.GetUtcNow();

            var caseNumber = $"BLT-{createdAtUtc:yyyyMMdd}-{token[..8].ToUpperInvariant()}";

            var amplificationCase = new AmplificationCase(
                Token: token,
                CaseNumber: caseNumber,
                OriginalUrl: normalizedUrl,
                AmplifiedRelativeUrl: BuildAmplifiedRelativeUrl(
                    token,
                    caseNumber),
                CreatedAtUtc: createdAtUtc);

            if (await repository.TryAddAsync(amplificationCase, cancellationToken))
            {
                return amplificationCase;
            }
        }

        throw new InvalidOperationException(
            "The case-number allocation subsystem exhausted its approved " +
            "retry allowance. Form BLT-EXC-14 must now be completed manually.");
    }

    private static string CreateToken()
    {
        Span<byte> tokenBytes = stackalloc byte[32];

        RandomNumberGenerator.Fill(tokenBytes);

        return Convert
            .ToHexString(tokenBytes)
            .ToLowerInvariant();
    }

    private static string BuildAmplifiedRelativeUrl(string token, string caseNumber)
    {
        return string.Concat(
            PublicRouteBase,
            "/",
            token,
            "?caseNumber=",
            Uri.EscapeDataString(caseNumber),
            "&workflowPhase=preliminary-approval-complete",
            "&interdepartmentalRoutingStatus=pending",
            "&complianceReviewDisposition=no-objection-recorded",
            "&minimumRequiredFriction=restored");
    }
}