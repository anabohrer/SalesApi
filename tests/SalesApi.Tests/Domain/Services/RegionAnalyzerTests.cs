using FluentAssertions;
using SalesApi.Domain.Services;

namespace SalesApi.Tests.Domain.Services
{
    public class RegionAnalyzerTests
    {
        private readonly RegionAnalyzer _regionAnalyzer;

        public RegionAnalyzerTests()
        {
            _regionAnalyzer = new RegionAnalyzer();
        }

        [Fact]
        public void GivenSingleRegion_WhenGetMostCommonRegion_ThenReturnsThatRegion()
        {
            // Given
            var regions = new[] { "North America" };

            // When
            var result = _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            result.Should().Be("North America");
        }

        [Fact]
        public void GivenMultipleRegionsWithClearWinner_WhenGetMostCommonRegion_ThenReturnsMostFrequent()
        {
            // Given
            var regions = new[]
            {
                "Europe", "North America", "Europe", "Asia", "Europe"
            };

            // When
            var result = _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            result.Should().Be("Europe");
        }

        [Fact]
        public void GivenMultipleRegionsWithTie_WhenGetMostCommonRegion_ThenReturnsAlphabeticallyFirst()
        {
            // Given
            var regions = new[]
            {
                "Europe", "Asia", "North America", "Europe", "Asia", "North America"
            };

            // When
            var result = _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            result.Should().Be("Asia");
        }

        [Fact]
        public void GivenRegionsWithDifferentCasing_WhenGetMostCommonRegion_ThenTreatsThemAsDifferent()
        {
            // Given
            var regions = new[]
            {
                "europe", "Europe", "EUROPE", "europe"
            };

            // When
            var result = _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            result.Should().Be("europe");
        }

        [Fact]
        public void GivenIdenticalRegions_WhenGetMostCommonRegion_ThenReturnsTheRegion()
        {
            // Given
            var regions = new[]
            {
                "Asia", "Asia", "Asia", "Asia"
            };

            // When
            var result = _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            result.Should().Be("Asia");
        }

        [Fact]
        public void GivenComplexTieScenario_WhenGetMostCommonRegion_ThenReturnsAlphabeticallyFirstAmongTied()
        {
            // Given
            var regions = new[]
            {
                "Oceania", "Africa", "Europe", "Oceania", "Africa", "South America"
            };

            // When
            var result = _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            result.Should().Be("Africa");
        }

        [Fact]
        public void GivenRegionsWithSpecialCharacters_WhenGetMostCommonRegion_ThenHandlesCorrectly()
        {
            // Given
            var regions = new[]
            {
                "North-America", "South América", "North-America", "Europe & Middle East"
            };

            // When
            var result = _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            result.Should().Be("North-America");
        }

        [Fact]
        public void GivenEmptyCollection_WhenGetMostCommonRegion_ThenThrowsArgumentException()
        {
            // Given
            var regions = Enumerable.Empty<string>();

            // When
            Action act = () => _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            act.Should().Throw<ArgumentException>()
                .WithMessage("No valid regions provided*")
                .And.ParamName.Should().Be("regions");
        }

        [Fact]
        public void GivenNullCollection_WhenGetMostCommonRegion_ThenThrowsArgumentNullException()
        {
            // Given
            IEnumerable<string> regions = null!;

            // When
            Action act = () => _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            act.Should().Throw<ArgumentNullException>()
                .And.ParamName.Should().Be("regions");
        }

        [Fact]
        public void GivenRegionsWithWhitespace_WhenGetMostCommonRegion_ThenTreatsWhitespaceAsPartOfRegionName()
        {
            // Given
            var regions = new[]
            {
                "North America", " North America", "North America ", "North America"
            };

            // When
            var result = _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            result.Should().Be("North America");
        }

        [Fact]
        public void GivenLargeCollectionOfRegions_WhenGetMostCommonRegion_ThenPerformsEfficiently()
        {
            // Given
            var regions = new List<string>();
            for (int i = 0; i < 1000; i++)
            {
                regions.Add("Europe");
                regions.Add("Asia");
            }
            regions.Add("North America");

            // When
            var result = _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            result.Should().Be("Asia");
        }

        [Fact]
        public void GivenRegionsWithEmptyStrings_WhenGetMostCommonRegion_ThenFiltersOutEmptyStringsAndReturnsValidRegion()
        {
            // Given
            var regions = new[]
            {
                "", "Europe", "", "Asia", "Europe"
            };

            // When
            var result = _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            result.Should().Be("Europe");
        }

        [Fact]
        public void GivenRegionsWithOnlyWhitespace_WhenGetMostCommonRegion_ThenFiltersOutWhitespaceOnlyStrings()
        {
            // Given
            var regions = new[]
            {
                "   ", "Europe", "\t", "Asia", "Europe", "\n  \r"
            };

            // When
            var result = _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            result.Should().Be("Europe");
        }

        [Fact]
        public void GivenOnlyEmptyAndWhitespaceRegions_WhenGetMostCommonRegion_ThenThrowsArgumentException()
        {
            // Given
            var regions = new[]
            {
                "", "   ", "\t", "\n\r", "  "
            };

            // When
            Action act = () => _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            act.Should().Throw<ArgumentException>()
                .WithMessage("No valid regions provided*")
                .And.ParamName.Should().Be("regions");
        }

        [Fact]
        public void GivenMixOfValidAndInvalidRegions_WhenGetMostCommonRegion_ThenIgnoresInvalidAndProcessesValid()
        {
            // Given
            var regions = new[]
            {
                "Asia", "", "Europe", "   ", "Asia", "\t", "North America", ""
            };

            // When
            var result = _regionAnalyzer.GetMostCommonRegion(regions);

            // Then
            result.Should().Be("Asia");
        }
    }
}
