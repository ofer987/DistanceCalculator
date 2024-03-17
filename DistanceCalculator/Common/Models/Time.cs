using System.Text.Json.Serialization;

namespace DistanceCalculator.Common.Models;

public class Time
{
    [JsonPropertyName("arrival_time")]
    public required string ArrivalTime { get; init; }

    [JsonPropertyName("departure_time")]
    public required string DepartureTime { get; init; }
}
