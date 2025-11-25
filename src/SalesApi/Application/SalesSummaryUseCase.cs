using SalesApi.Domain.Models;
using SalesApi.Domain.Services;

namespace SalesApi.Application;

public sealed class SalesSummaryUseCase(
    ISalesDataSource salesDataSource,
    IMedianCalculator medianCalculator,
    IDateRangeCalculator dateRangeCalculator) : ISalesSummaryUseCase
{
    private readonly ISalesDataSource salesDataSource = salesDataSource;
    private readonly IMedianCalculator medianCalculator = medianCalculator;
    private readonly IDateRangeCalculator dateRangeCalculator = dateRangeCalculator;

    public async Task<SummaryResult> ComputeSummaryAsync(Stream csvStream, CancellationToken cancellationToken = default)
    {
        var records = (await salesDataSource.ReadAllAsync(csvStream, cancellationToken)).ToList();

        if (records.Count == 0)
            throw new InvalidOperationException("No records found in CSV.");

        var medianUnitCost = medianCalculator.ComputeMedian(records.Select(r => r.UnitCost));

        var mostCommonRegion = records
            .GroupBy(r => r.Region)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key, StringComparer.Ordinal)
            .First().Key;

        var (first, last) = dateRangeCalculator.GetFirstAndLast(records.Select(r => r.OrderDate));
        var daysBetween = (int)(last - first).TotalDays;

        var totalRevenue = records.Sum(r => r.TotalRevenue);

        return new SummaryResult
        {
            MedianUnitCost = medianUnitCost,
            MostCommonRegion = mostCommonRegion,
            FirstOrderDate = first,
            LastOrderDate = last,
            DaysBetweenFirstAndLast = daysBetween,
            TotalRevenue = totalRevenue
        };
    }
}
