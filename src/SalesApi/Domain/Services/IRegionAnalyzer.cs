namespace SalesApi.Domain.Services;

/// <summary>
/// Service for analyzing regional data patterns.
/// </summary>
public interface IRegionAnalyzer
{
    /// <summary>
    /// Determines the most frequently occurring region from a collection.
    /// In case of a tie, returns the region that comes first alphabetically.
    /// Empty or whitespace-only strings are filtered out as invalid regions.
    /// </summary>
    /// <param name="regions">Collection of region names to analyze.</param>
    /// <returns>The most common region name.</returns>
    /// <exception cref="ArgumentNullException">Thrown when the regions collection is null.</exception>
    /// <exception cref="ArgumentException">Thrown when the regions collection is empty or contains no valid regions.</exception>
    string GetMostCommonRegion(IEnumerable<string> regions);
}
