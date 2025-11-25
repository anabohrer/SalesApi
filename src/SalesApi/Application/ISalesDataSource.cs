using SalesApi.Domain.Models;

namespace SalesApi.Application;

public interface ISalesDataSource
{
    Task<IEnumerable<SalesRecord>> ReadAllAsync(Stream csvStream, CancellationToken cancellationToken = default);
}
