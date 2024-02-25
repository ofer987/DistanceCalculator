using System.Text;
using System.Text.Json.Serialization;

public record Branch
{
    [JsonRequired]
    [JsonPropertyName("route")]
    public required RouteId RouteId { get; init; }

    [JsonRequired]
    [JsonPropertyName("routeBranchesWithStops")]
    public required IList<Route> Routes { get; init; } = new List<Route>();

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"{nameof(Routes)} = ");
        foreach (var route in Routes)
        {
            stringBuilder.AppendLine($"{route}");
        }

        return stringBuilder.ToString();
    }
}
