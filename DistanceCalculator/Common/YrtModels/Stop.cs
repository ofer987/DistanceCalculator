using CsvHelper.Configuration.Attributes;

namespace DistanceCalculator.YrtModels;

public class Stop
{
    [Name("stop_id")]
    public int Id { get; init; }

    [Name("stop_code")]
    [Optional]
    public int? Code { get; init; }

    [Name("stop_name")]
    public string Name { get; init; } = string.Empty;

    [Name("stop_lat")]
    public double Latitude { get; init; }

    [Name("stop_lon")]
    public double Longitude { get; init; }

    // public string GetName()
    // {
    //     if (ShortName is not null) {
    //         return ShortName;
    //     }
    //
    //     if (LongName is not null) {
    //         return LongName;
    //     }
    //
    //     throw new NullReferenceException($"The {nameof(Stop)} class does not contain either a  {nameof(ShortName)} or a  {nameof(LongName)} property");
    // }
}
