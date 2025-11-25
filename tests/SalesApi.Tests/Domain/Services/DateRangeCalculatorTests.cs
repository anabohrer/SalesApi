using FluentAssertions;
using SalesApi.Domain.Services;

namespace SalesApi.Tests.Domain.Services
{
    public class DateRangeCalculatorTests
    {
        private readonly DateRangeCalculator dateRangeCalculator;

        public DateRangeCalculatorTests()
        {
            dateRangeCalculator = new DateRangeCalculator();
        }

        [Fact]
        public void GivenDates_WhenGettingFirstAndLast_ThenCorrectValuesReturned()
        {
            // Given
            var dates = new[]
            {
                new DateTime(2023, 1, 15),
                new DateTime(2022, 12, 31),
                new DateTime(2023, 6, 1),
                new DateTime(2023, 3, 10)
            };
            
            // When
            var (first, last) = dateRangeCalculator.GetFirstAndLast(dates);
            
            // Then
            first.Should().Be(new DateTime(2022, 12, 31));
            last.Should().Be(new DateTime(2023, 6, 1));
        }

        [Fact]
        public void GivenEmptyDates_WhenGettingFirstAndLast_ThenArgumentExceptionThrown()
        {
            // Given
            var dates = Array.Empty<DateTime>();
            
            // When
            Action act = () => dateRangeCalculator.GetFirstAndLast(dates);
            
            // Then
            act.Should().Throw<ArgumentException>()
                .WithMessage("No dates provided*")
                .And.ParamName.Should().Be("dates");
        }
    }
}
