using SalesApi.Domain.Models;

namespace SalesApi.Application;

public interface ISalesSummaryUseCase
{
    Task<SummaryResult> ComputeSummaryAsync(Stream csvStream, CancellationToken cancellationToken = default);
}
