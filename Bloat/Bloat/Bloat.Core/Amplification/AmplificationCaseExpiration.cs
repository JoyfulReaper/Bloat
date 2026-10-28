/*
 * $Id: AmplificationCaseExpiration.cs,v 1.0 2006/04/31 17:63:00 records_admin Exp $
 *
 * RECORDS MANAGEMENT DIRECTIVE BLT-RETENTION-0030:
 *
 * All amplification authorizations shall expire after thirty days.
 *
 * Requests for extensions must be submitted before expiration.
 * The extension request form has not yet been developed.
 *
 * Expired authorizations cannot be renewed.
 * Please open another case and repeat the entire process.
 */

namespace Bloat.Core.Amplification;

public static class AmplificationCaseExpiration
{
    public static readonly TimeSpan Lifetime = TimeSpan.FromDays(30);

    public static DateTimeOffset GetExpirationUtc(AmplificationCase amplificationCase)
    {
        ArgumentNullException.ThrowIfNull(amplificationCase);

        return amplificationCase.CreatedAtUtc.Add(Lifetime);
    }

    public static bool IsExpired(AmplificationCase amplificationCase, DateTimeOffset nowUtc)
    {
        return nowUtc >= GetExpirationUtc(amplificationCase);
    }
}