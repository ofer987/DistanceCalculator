using System.Text.Json.Serialization;

namespace DistanceCalculator.Common.Models;

// public enum LineTypes { Subway = 0, Bus, Streetcar };

public class Trip
{
    // [JsonPropertyName("type")]
    // public LineTypes Type { get; init; } = LineTypes.Bus;

    // [JsonPropertyName("agencyName")]
    // public required string Agency { get; init; }

    [JsonPropertyName("id")]
    public required int Id { get; init; }

    [JsonPropertyName("trip_headsign")]
    public required string Name { get; init; }

    [JsonPropertyName("route_id")]
    public int LineId => Line.Id;

    [JsonPropertyName("stop_id")]
    public int StopId => Stop.Id;

    [JsonIgnore]
    public Line? Line { get; init; } = null;

    [JsonIgnore]
    public Stop? Stop { get; init; } = null;

    [JsonPropertyName("schedule")]
    public IList<Time> Schedule { get; init; } = new List<Time>();
}
