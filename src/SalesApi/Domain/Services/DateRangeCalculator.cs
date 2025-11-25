namespace SalesApi.Domain.Services;

public sealed class DateRangeCalculator : IDateRangeCalculator
{
    public (DateTime First, DateTime Last) GetFirstAndLast(IEnumerable<DateTime> dates)
    {
        var list = dates.ToList();
        if (list.Count == 0)
            throw new ArgumentException("No dates provided", nameof(dates));

        return (list.Min(), list.Max());
    }
}
