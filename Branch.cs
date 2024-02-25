using System.Text;
using System.Text.Json.Serialization;

public record Branch
{
    [JsonRequired]
    [JsonPropertyName("id")]
    public required int Id { get; init; }

    [JsonRequired]
    [JsonPropertyName("routeBranch")]
    public required Direction Direction { get; init; } = new Direction();

    [JsonRequired]
    [JsonPropertyName("routeBranchStops")]
    public required IList<Stop> Stops { get; init; } = new Stop[0];

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"{nameof(Id)} = {Id}");
        stringBuilder.AppendLine($"{nameof(Direction)} = {Direction}");

        foreach (var stop in Stops)
        {
            stringBuilder.AppendLine($"{stop}");
        }

        return stringBuilder.ToString();
    }
}
