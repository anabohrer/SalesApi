namespace SalesApi.Domain.Services;

public interface IDateRangeCalculator
{
    (DateTime First, DateTime Last) GetFirstAndLast(IEnumerable<DateTime> dates);
}
