using CsvHelper.Configuration.Attributes;

namespace DistanceCalculator.YrtModels;

public class Route
{
    [Name("route_id")]
    public int Id { get; init; }

    [Name("route_short_name")]
    public string? ShortName { get; init; }

    [Name("route_long_name")]
    public string? LongName { get; init; }

    public string GetName()
    {
        if (ShortName is not null) {
            return ShortName;
        }

        if (LongName is not null) {
            return LongName;
        }

        throw new NullReferenceException($"The {nameof(Route)} class does not contain either a  {nameof(ShortName)} or a  {nameof(LongName)} property");
    }
}
