using FluentAssertions;
using SalesApi.Domain.Models;

namespace SalesApi.Tests.Domain.Models
{
    public sealed class SummaryResultTests
    {
        [Fact]
        public void GivenNewSummaryResult_WhenCreated_ThenAllPropertiesHaveDefaultValues()
        {
            // Given & When
            var summaryResult = new SummaryResult();

            // Then
            summaryResult.MedianUnitCost.Should().Be(0);
            summaryResult.MostCommonRegion.Should().Be(string.Empty);
            summaryResult.FirstOrderDate.Should().Be(default);
            summaryResult.LastOrderDate.Should().Be(default);
            summaryResult.DaysBetweenFirstAndLast.Should().Be(0);
            summaryResult.TotalRevenue.Should().Be(0);
        }

        [Fact]
        public void GivenSummaryResult_WhenSettingMedianUnitCost_ThenPropertyIsSetCorrectly()
        {
            // Given
            var summaryResult = new SummaryResult();
            const decimal medianUnitCost = 125.75m;

            // When
            summaryResult.MedianUnitCost = medianUnitCost;

            // Then
            summaryResult.MedianUnitCost.Should().Be(medianUnitCost);
        }

        [Fact]
        public void GivenSummaryResult_WhenSettingMostCommonRegion_ThenPropertyIsSetCorrectly()
        {
            // Given
            var summaryResult = new SummaryResult();
            const string mostCommonRegion = "North America";

            // When
            summaryResult.MostCommonRegion = mostCommonRegion;

            // Then
            summaryResult.MostCommonRegion.Should().Be(mostCommonRegion);
        }

        [Fact]
        public void GivenSummaryResult_WhenSettingDateProperties_ThenPropertiesAreSetCorrectly()
        {
            // Given
            var summaryResult = new SummaryResult();
            var firstOrderDate = new DateTime(2023, 1, 15);
            var lastOrderDate = new DateTime(2023, 12, 20);

            // When
            summaryResult.FirstOrderDate = firstOrderDate;
            summaryResult.LastOrderDate = lastOrderDate;

            // Then
            summaryResult.FirstOrderDate.Should().Be(firstOrderDate);
            summaryResult.LastOrderDate.Should().Be(lastOrderDate);
        }

        [Fact]
        public void GivenSummaryResult_WhenSettingDaysBetweenFirstAndLast_ThenPropertyIsSetCorrectly()
        {
            // Given
            var summaryResult = new SummaryResult();
            const int daysBetween = 339;

            // When
            summaryResult.DaysBetweenFirstAndLast = daysBetween;

            // Then
            summaryResult.DaysBetweenFirstAndLast.Should().Be(daysBetween);
        }

        [Fact]
        public void GivenSummaryResult_WhenSettingTotalRevenue_ThenPropertyIsSetCorrectly()
        {
            // Given
            var summaryResult = new SummaryResult();
            const decimal totalRevenue = 1_250_000.99m;

            // When
            summaryResult.TotalRevenue = totalRevenue;

            // Then
            summaryResult.TotalRevenue.Should().Be(totalRevenue);
        }

        [Fact]
        public void GivenSummaryResult_WhenSettingAllProperties_ThenAllPropertiesAreSetCorrectly()
        {
            // Given
            var summaryResult = new SummaryResult();
            const decimal medianUnitCost = 89.50m;
            const string mostCommonRegion = "Europe";
            var firstOrderDate = new DateTime(2023, 3, 1);
            var lastOrderDate = new DateTime(2023, 11, 30);
            const int daysBetween = 274;
            const decimal totalRevenue = 2_750_500.25m;

            // When
            summaryResult.MedianUnitCost = medianUnitCost;
            summaryResult.MostCommonRegion = mostCommonRegion;
            summaryResult.FirstOrderDate = firstOrderDate;
            summaryResult.LastOrderDate = lastOrderDate;
            summaryResult.DaysBetweenFirstAndLast = daysBetween;
            summaryResult.TotalRevenue = totalRevenue;

            // Then
            summaryResult.MedianUnitCost.Should().Be(medianUnitCost);
            summaryResult.MostCommonRegion.Should().Be(mostCommonRegion);
            summaryResult.FirstOrderDate.Should().Be(firstOrderDate);
            summaryResult.LastOrderDate.Should().Be(lastOrderDate);
            summaryResult.DaysBetweenFirstAndLast.Should().Be(daysBetween);
            summaryResult.TotalRevenue.Should().Be(totalRevenue);
        }

        [Fact]
        public void GivenSummaryResult_WhenSettingZeroValues_ThenPropertiesAcceptZeroValues()
        {
            // Given
            var summaryResult = new SummaryResult();
            const decimal zeroDecimal = 0m;
            const int zeroInt = 0;

            // When
            summaryResult.MedianUnitCost = zeroDecimal;
            summaryResult.DaysBetweenFirstAndLast = zeroInt;
            summaryResult.TotalRevenue = zeroDecimal;

            // Then
            summaryResult.MedianUnitCost.Should().Be(zeroDecimal);
            summaryResult.DaysBetweenFirstAndLast.Should().Be(zeroInt);
            summaryResult.TotalRevenue.Should().Be(zeroDecimal);
        }

        [Fact]
        public void GivenSummaryResult_WhenSettingNegativeValues_ThenPropertiesAcceptNegativeValues()
        {
            // Given
            var summaryResult = new SummaryResult();
            const decimal negativeMedianCost = -50.25m;
            const int negativeDays = -10;
            const decimal negativeTotalRevenue = -1000.00m;

            // When
            summaryResult.MedianUnitCost = negativeMedianCost;
            summaryResult.DaysBetweenFirstAndLast = negativeDays;
            summaryResult.TotalRevenue = negativeTotalRevenue;

            // Then
            summaryResult.MedianUnitCost.Should().Be(negativeMedianCost);
            summaryResult.DaysBetweenFirstAndLast.Should().Be(negativeDays);
            summaryResult.TotalRevenue.Should().Be(negativeTotalRevenue);
        }
    }
}
