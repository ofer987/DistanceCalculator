using System.Text;
using System.Text.Json.Serialization;

public class Stop : Serializable
{
    [JsonRequired]
    [JsonPropertyName("id")]
    public required int Id { get; init; }

    [JsonRequired]
    [JsonPropertyName("name")]
    public required string Name { get; init; }

    [JsonRequired]
    [JsonPropertyName("latitude")]
    public required float Latitude { get; init; }

    [JsonRequired]
    [JsonPropertyName("longitude")]
    public required float Longitude { get; init; }

    [JsonRequired]
    [JsonPropertyName("routeStopTimes")]
    public required IList<string> Times { get; init; } = new string[0];

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"{nameof(Id)} = {Id}");
        stringBuilder.AppendLine($"{nameof(Name)} = {Name}");
        stringBuilder.AppendLine($"{nameof(Latitude)} = {Latitude}");
        stringBuilder.AppendLine($"{nameof(Longitude)} = {Longitude}");

        stringBuilder.AppendLine($"{nameof(Times)} =");
        foreach (var time in Times)
        {
            stringBuilder.AppendLine($"{time}");
        }

        return stringBuilder.ToString();
    }
}
