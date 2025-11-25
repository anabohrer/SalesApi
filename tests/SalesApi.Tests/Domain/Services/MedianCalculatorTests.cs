using FluentAssertions;
using SalesApi.Domain.Services;

namespace SalesApi.Tests.Domain.Services;

public sealed class MedianCalculatorTests
{
    private readonly MedianCalculator medianCalculator;
    public MedianCalculatorTests()
    {
        medianCalculator = new MedianCalculator();
    }

    [Fact]
    public void GivenEmptySequence_WhenComputingMedian_ThenZeroReturned()
    {
        var result = medianCalculator.ComputeMedian([]);
        result.Should().Be(0m);
    }

    [Fact]
    public void GivenOddCount_WhenComputingMedian_ThenMiddleValueReturned()
    {
        var values = new[] { 10m, 30m, 97.44m };
        var result = medianCalculator.ComputeMedian(values);
        result.Should().Be(30m);
    }

    [Fact]
    public void GivenEvenCount_WhenComputingMedian_ThenAverageOfMiddleTwoReturned()
    {
        var values = new[] { 20m, 30m };
        var result = medianCalculator.ComputeMedian(values);
        result.Should().Be(25m);
    }
}
