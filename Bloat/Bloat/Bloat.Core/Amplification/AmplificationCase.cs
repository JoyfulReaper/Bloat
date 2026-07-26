/*
 * $Id: AmplificationCase.cs,v 5.4 2008/02/19 11:48:06 records_admin Exp $
 * $Source: /CorporateSystems/BLOAT/Core/CaseManagement/AmplificationCase.cs $
 *
 * RECORDS RETENTION NOTICE:
 * Amplification cases must be retained for seven years or until the
 * application process exits, whichever occurs first.
 *
 * The original database password was removed from this header after
 * source control was declared a "shared information environment."
 */

namespace Bloat.Core.Amplification;

/// <summary>
/// Represents an approved external-resource amplification case.
/// </summary>
public sealed record AmplificationCase(
    string Token,
    string CaseNumber,
    string OriginalUrl,
    string AmplifiedRelativeUrl,
    DateTimeOffset CreatedAtUtc);