using System.Text;
using System.Text.Json.Serialization;

public class Route : Serializable
{
    [JsonRequired]
    [JsonPropertyName("route")]
    public required RouteInformation Information { get; init; }

    [JsonRequired]
    [JsonPropertyName("routeBranchesWithStops")]
    public required IList<Branch> Branches { get; init; } = new List<Branch>();

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();
        stringBuilder.AppendLine($"{nameof(Branches)} = ");
        foreach (var branch in Branches)
        {
            stringBuilder.AppendLine($"{branch}");
        }

        return stringBuilder.ToString();
    }
}
