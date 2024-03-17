using System.Text.Json.Serialization;

namespace DistanceCalculator.Common.Models;

public enum LineTypes { Subway = 0, Bus, Streetcar };

public class Line
{
    [JsonPropertyName("type")]
    public virtual LineTypes Type { get; init; } = LineTypes.Bus;

    [JsonPropertyName("agencyName")]
    public required string Agency { get; init; }

    [JsonPropertyName("id")]
    public required int Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("fullName")]
    public string FullName => $"{Id} - {Name}";

    [JsonIgnore]
    public IList<Stop> Stops { get; } = new List<Stop>();
}
