/*
 * $Id: InMemoryAmplificationCaseRepository.cs,v 0.4 2010/01/06 02:13:08 disaster_recovery Exp $
 *
 * TEMPORARY STORAGE AUTHORIZATION:
 * Approved for temporary use during the 2010 database migration.
 *
 * Migration status:
 *     Not located.
 *
 * OPERATIONAL WARNING:
 * Restarting the application constitutes a complete records-retention event.
 */

using Bloat.Core.Amplification;
using System.Collections.Concurrent;

namespace Bloat.Data.Amplification;

public sealed class InMemoryAmplificationCaseRepository : IAmplificationCaseRepository
{
    private readonly ConcurrentDictionary<string, AmplificationCase> _cases = new(StringComparer.Ordinal);

    public ValueTask<bool> TryAddAsync(AmplificationCase amplificationCase, CancellationToken cancellationToken = default)
    {
        ArgumentNullException.ThrowIfNull(amplificationCase);
        cancellationToken.ThrowIfCancellationRequested();

        var added = _cases.TryAdd(amplificationCase.Token, amplificationCase);

        return ValueTask.FromResult(added);
    }

    public ValueTask<AmplificationCase?> FindByTokenAsync(string token, CancellationToken cancellationToken = default)
    {
        ArgumentException.ThrowIfNullOrWhiteSpace(token);
        cancellationToken.ThrowIfCancellationRequested();

        _cases.TryGetValue(token, out var amplificationCase);

        return ValueTask.FromResult(amplificationCase);
    }
}