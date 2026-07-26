/*
 * $Id: IAmplificationCaseRepository.cs,v 1.9 2008/02/21 06:17:42 architect_temp Exp $
 *
 * ARCHITECTURAL NOTE:
 * The repository abstraction was introduced before the storage technology
 * was selected so that the storage selection committee could continue
 * meeting without delaying implementation.
 */

namespace Bloat.Core.Amplification;

public interface IAmplificationCaseRepository
{
    ValueTask<bool> TryAddAsync(
        AmplificationCase amplificationCase,
        CancellationToken cancellationToken = default);

    ValueTask<AmplificationCase?> FindByTokenAsync(
        string token,
        CancellationToken cancellationToken = default);
}