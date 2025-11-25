namespace SalesApi.Domain.Models;

public sealed class SalesRecord
{
    public string Region { get; set; } = string.Empty;
    public string Country { get; set; } = string.Empty;
    public string ItemType { get; set; } = string.Empty;
    public string SalesChannel { get; set; } = string.Empty;
    public string OrderPriority { get; set; } = string.Empty;
    public DateTime OrderDate { get; set; }
    public long OrderID { get; set; }
    public DateTime ShipDate { get; set; }
    public int UnitsSold { get; set; }
    public decimal UnitPrice { get; set; }
    public decimal UnitCost { get; set; }
    public decimal TotalRevenue { get; set; }
    public decimal TotalCost { get; set; }
    public decimal TotalProfit { get; set; }
}
