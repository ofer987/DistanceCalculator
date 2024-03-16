using CsvHelper.Configuration.Attributes;

namespace DistanceCalculator.YrtModels;

public class StopTime
{
    [Name("trip_id")]
    public int Id { get; init; }

    [Name("arrival_time")]
    public string ArrivalTime { get; init; } = string.Empty;

    [Name("departure_time")]
    public string DepartureTime { get; init; } = string.Empty;

    [Name("stop_id")]
    public int StopId { get; init; }
}
