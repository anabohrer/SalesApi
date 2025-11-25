namespace SalesApi.Domain.Services;

/// <summary>
/// Service for analyzing regional data patterns.
/// </summary>
public sealed class RegionAnalyzer : IRegionAnalyzer
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
    public string GetMostCommonRegion(IEnumerable<string> regions)
    {
        ArgumentNullException.ThrowIfNull(regions);

        var validRegions = regions
            .Where(r => !string.IsNullOrWhiteSpace(r))
            .ToList();

        if (validRegions.Count == 0)
            throw new ArgumentException("No valid regions provided", nameof(regions));

        return validRegions
            .GroupBy(r => r)
            .OrderByDescending(g => g.Count())
            .ThenBy(g => g.Key, StringComparer.Ordinal)
            .First()
            .Key;
    }
}
