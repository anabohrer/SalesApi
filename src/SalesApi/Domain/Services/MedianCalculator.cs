namespace SalesApi.Domain.Services;

public sealed class MedianCalculator : IMedianCalculator
{
    public decimal ComputeMedian(IEnumerable<decimal> values)
    {
        var sorted = values.OrderBy(x => x).ToList();
        int n = sorted.Count;
        if (n == 0) return 0m;
        if (n % 2 == 1) return sorted[n / 2];
        var a = sorted[n / 2 - 1];
        var b = sorted[n / 2];
        return (a + b) / 2;
    }
}
