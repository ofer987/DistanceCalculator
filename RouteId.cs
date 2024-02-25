using System.Text;
using System.Text.Json.Serialization;

public record RouteId
{
    [JsonRequired]
    [JsonPropertyName("id")]
    public required int Id { get; init; }

    [JsonRequired]
    [JsonPropertyName("shortName")]
    public required String ShortName { get; init; }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"{nameof(Id)} = {Id}");
        stringBuilder.AppendLine($"{nameof(ShortName)} = {ShortName}");

        return stringBuilder.ToString();
    }
}
