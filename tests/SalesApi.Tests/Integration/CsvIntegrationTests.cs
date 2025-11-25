using System.Text;
using FluentAssertions;
using SalesApi.Infrastructure.Csv;

namespace SalesApi.Tests.Integration;

public sealed class CsvIntegrationTests
{
    [Fact]
    public async Task GivenCsvStream_WhenReadingRecords_ThenRecordsParsedCorrectly()
    {
        // Given
        var csv = new StringBuilder();
        csv.AppendLine("Region,Country,Item Type,Sales Channel,Order Priority,Order Date,Order ID,Ship Date,Units Sold,Unit Price,Unit Cost,Total Revenue,Total Cost,Total Profit");
        csv.AppendLine("Asia,China,Clothing,Online,L,3/3/2016,987654321,3/10/2016,200,20.00,10.00,4000.00,2000.00,2000.00");

        using var ms = new MemoryStream(Encoding.UTF8.GetBytes(csv.ToString()));
        var source = new CsvSalesDataSource();

        // When
        var records = (await source.ReadAllAsync(ms)).ToList();

        // Then
        records.Should().HaveCount(1);
        records[0].Region.Should().Be("Asia");
        records[0].UnitCost.Should().Be(10.00m);
        records[0].OrderDate.Should().Be(new DateTime(2016, 3, 3));
        records[0].TotalRevenue.Should().Be(4000.00m);
    }
}
