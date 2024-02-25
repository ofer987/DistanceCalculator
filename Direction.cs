using System.Text.Json.Serialization;

public record Direction
{
    [JsonRequired]
    [JsonPropertyName("branchName")]
    public string BranchName { get; init; } = string.Empty;

    [JsonRequired]
    [JsonPropertyName("headsign")]
    public string HeadSign { get; init; } = string.Empty;

    public override string ToString() {
        return $"{nameof(BranchName)} = {BranchName}";
    }
}
