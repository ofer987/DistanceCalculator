using System.Text;
using System.Text.Json.Serialization;

public class Direction : Serializable
{
    [JsonRequired]
    [JsonPropertyName("branchName")]
    public string BranchName { get; init; } = string.Empty;

    [JsonRequired]
    [JsonPropertyName("headsign")]
    public string HeadSign { get; init; } = string.Empty;

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"{nameof(BranchName)} = {BranchName}");
        sb.AppendLine($"{nameof(HeadSign)} = {HeadSign}");

        return sb.ToString();
    }
}
