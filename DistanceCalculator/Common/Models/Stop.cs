using System.Text;
using System.Text.Json.Serialization;

using Geolocation;

namespace DistanceCalculator.Common.Models;

public class Stop
{
    [JsonPropertyName("trip")]
    public IList<Trip> Trips { get; init; } = new List<Trip>();

    // TODO: change to init with JSONIgnore
    [JsonPropertyName("line")]
    public required Line Line { get; init; }

    [JsonPropertyName("lineId")]
    public int LineId => Line.Id;

    [JsonPropertyName("id")]
    public required int Id { get; init; }

    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonPropertyName("latitude")]
    public required double Latitude { get; init; }

    [JsonPropertyName("longitude")]
    public required double Longitude { get; init; }

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
