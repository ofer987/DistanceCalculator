using System.Text;
using System.Text.Json.Serialization;

public record Branch
{
    [JsonRequired]
    [JsonPropertyName("routeBranchesWithStops")]
    public required IList<Route> Routes { get; init; } = new Route[0];

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
