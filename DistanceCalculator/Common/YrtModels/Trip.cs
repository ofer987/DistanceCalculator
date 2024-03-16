using CsvHelper.Configuration.Attributes;

namespace DistanceCalculator.YrtModels;

public class Trip
{
    [Name("trip_id")]
    public int Id { get; init; }

    [Name("route_id")]
    public int RouteId { get; init; }

    [Name("trip_short_name")]
    public string TripShortName { get; init; } = string.Empty;
}
