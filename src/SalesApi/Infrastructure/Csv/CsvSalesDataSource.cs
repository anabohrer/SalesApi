using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using SalesApi.Application;
using SalesApi.Domain.Models;

namespace SalesApi.Infrastructure.Csv;

public sealed class CsvSalesDataSource : ISalesDataSource
{
    private static readonly string[] DateFormats = ["M/d/yyyy", "MM/dd/yyyy", "yyyy-MM-dd", "M/d/yyyy h:mm:ss tt", "M/d/yyyy H:mm:ss"];

    public async Task<IEnumerable<SalesRecord>> ReadAllAsync(Stream csvStream, CancellationToken cancellationToken = default)
    {
        using var reader = new StreamReader(csvStream);
        using var csv = new CsvReader(reader, new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            MissingFieldFound = null,
            BadDataFound = null
        });

        csv.Context.RegisterClassMap<SalesRecordMap>();

        var records = new List<SalesRecord>();
        await foreach (var rec in csv.GetRecordsAsync<SalesRecord>().WithCancellation(cancellationToken))
        {
            records.Add(rec);
        }

        return records;
    }

    private sealed class SalesRecordMap : ClassMap<SalesRecord>
    {
        public SalesRecordMap()
        {
            Map(m => m.Region).Name("Region");
            Map(m => m.Country).Name("Country");
            Map(m => m.ItemType).Name("Item Type");
            Map(m => m.SalesChannel).Name("Sales Channel");
            Map(m => m.OrderPriority).Name("Order Priority");
            Map(m => m.OrderDate).Name("Order Date").TypeConverterOption.Format(DateFormats);
            Map(m => m.OrderID).Name("Order ID");
            Map(m => m.ShipDate).Name("Ship Date").TypeConverterOption.Format(DateFormats);
            Map(m => m.UnitsSold).Name("Units Sold");
            Map(m => m.UnitPrice).Name("Unit Price");
            Map(m => m.UnitCost).Name("Unit Cost");
            Map(m => m.TotalRevenue).Name("Total Revenue");
            Map(m => m.TotalCost).Name("Total Cost");
            Map(m => m.TotalProfit).Name("Total Profit");
        }
    }
}
