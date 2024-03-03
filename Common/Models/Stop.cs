using System.Text;

using Geolocation;

namespace DistanceCalculator.Common.Models;

public class Stop
{
    public required Line Line { get; init; }

    public required int Id { get; init; }

    public required string Name { get; init; }

    public required float Latitude { get; init; }

    public required float Longitude { get; init; }

    public double GetDistance(float latitude, float longitude)
    {
        return GeoCalculator.GetDistance(latitude, longitude, Latitude, Longitude, 1, DistanceUnit.Meters);
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"{nameof(Id)} = {Id}");
        stringBuilder.AppendLine($"{nameof(Name)} = {Name}");
        stringBuilder.AppendLine($"{nameof(Latitude)} = {Latitude}");
        stringBuilder.AppendLine($"{nameof(Longitude)} = {Longitude}");

        return stringBuilder.ToString();
    }
}
