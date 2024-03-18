using System.Text.Json.Serialization;

namespace DistanceCalculator.Common.Models;

// public enum LineTypes { Subway = 0, Bus, Streetcar };

public class Destination
{
    // [JsonPropertyName("type")]
    // public LineTypes Type { get; init; } = LineTypes.Bus;

    // [JsonPropertyName("agencyName")]
    // public required string Agency { get; init; }

    // [JsonPropertyName("id")]
    // public required int Id { get; init; }
    //
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("line_id")]
    public int LineId { get; init; }

    [JsonPropertyName("stop_id")]
    public int StopId { get; init; }

    [JsonPropertyName("schedule")]
    public IList<Time> Schedule { get; init; } = new List<Time>();
}
