namespace SalesApi.Domain.Models;

public sealed class SummaryResult
{
    public decimal MedianUnitCost { get; set; }
    public string MostCommonRegion { get; set; } = string.Empty;

    public DateTime FirstOrderDate { get; set; }
    public DateTime LastOrderDate { get; set; }
    public int DaysBetweenFirstAndLast { get; set; }

    public decimal TotalRevenue { get; set; }
}
