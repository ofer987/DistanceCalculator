using System.Text.Json.Serialization;

namespace DistanceCalculator.Common.Models;

public enum LineTypes { Subway = 0, Bus, Streetcar };

public abstract class Line
{
    [JsonPropertyName("type")]
    public abstract LineTypes Type { get; }

    [JsonPropertyName("agencyName")]
    public required string Agency { get; init; }

    [JsonPropertyName("id")]
    public required string Id { get; init; }

    public required string Name { get; init; }

    [JsonPropertyName("fullName")]
    public string FullName => $"{Id} - {Name}";

    [JsonIgnore]
    public IList<Stop> Stops { get; } = new List<Stop>();
}
