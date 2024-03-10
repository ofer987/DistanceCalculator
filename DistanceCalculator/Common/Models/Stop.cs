using System.Text;
using System.Text.Json.Serialization;

using Geolocation;

namespace DistanceCalculator.Common.Models;

public class Stop
{
    [JsonPropertyName("line")]
    public required Line Line { get; init; }

    [JsonPropertyName("id")]
    public required int Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("latitude")]
    public required float Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public required float Longitude { get; init; }

    public double GetDistance(float latitude, float longitude)
    {
        return GeoCalculator.GetDistance(latitude, longitude, Latitude, Longitude, 1, DistanceUnit.Meters);
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"{nameof(Id)} = {Id}");
        stringBuilder.AppendLine($"{nameof(Name)} = {Name}");
        stringBuilder.AppendLine($"{nameof(Latitude)} = {Latitude}");
        stringBuilder.AppendLine($"{nameof(Longitude)} = {Longitude}");

        return stringBuilder.ToString();
    }
}
