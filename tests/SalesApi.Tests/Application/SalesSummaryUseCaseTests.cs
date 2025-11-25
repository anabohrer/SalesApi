using FluentAssertions;
using SalesApi.Application;
using SalesApi.Domain.Models;
using SalesApi.Domain.Services;

namespace SalesApi.Tests.Application;

public sealed class SalesSummaryUseCaseTests
{
    private class FakeDataSource(IEnumerable<SalesRecord> records) : ISalesDataSource
    {
        private readonly IEnumerable<SalesRecord> _records = records;

        public Task<IEnumerable<SalesRecord>> ReadAllAsync(Stream csvStream, CancellationToken cancellationToken = default)
            => Task.FromResult(_records);
    }

    [Fact]
    public async Task GivenThreeRecords_WhenComputingSummary_ThenValuesMatchExpected()
    {
        // Given
        var records = new[]
        {
            new SalesRecord { Region = "Middle East and North Africa", OrderDate = new DateTime(2014,10,8), UnitCost = 97.44m, TotalRevenue = 142509.72m },
            new SalesRecord { Region = "Europe", OrderDate = new DateTime(2015,1,15), UnitCost = 30.00m, TotalRevenue = 5000.00m },
            new SalesRecord { Region = "Asia", OrderDate = new DateTime(2016,3,3), UnitCost = 10.00m, TotalRevenue = 4000.00m },
        };

        var medianCalculator = new MedianCalculator();
        var dateRangeCalculator = new DateRangeCalculator();
        var useCase = new SalesSummaryUseCase(new FakeDataSource(records), medianCalculator, dateRangeCalculator);

        // When
        var result = await useCase.ComputeSummaryAsync(Stream.Null);

        // Then
        result.MedianUnitCost.Should().Be(30.00m);
        result.MostCommonRegion.Should().Be("Asia");
        result.FirstOrderDate.Should().Be(new DateTime(2014, 10, 8));
        result.LastOrderDate.Should().Be(new DateTime(2016, 3, 3));
        result.TotalRevenue.Should().BeApproximately(151509.72m, 0.005m);
    }

    [Fact]
    public async Task GivenNoRecords_WhenComputingSummary_ThenThrowsInvalidOperationException()
    {
        // Given
        var medianCalculator = new MedianCalculator();
        var dateRangeCalculator = new DateRangeCalculator();
        var useCase = new SalesSummaryUseCase(new FakeDataSource([]), medianCalculator, dateRangeCalculator);

        // When / Then
        await Assert.ThrowsAsync<InvalidOperationException>(() => useCase.ComputeSummaryAsync(Stream.Null));
    }
}
