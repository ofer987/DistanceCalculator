using System.Text;

using Geolocation;

namespace DistanceCalculator.Models;

public class Stop
{
    public required int Id { get; init; }

    public required string Name { get; init; }

    public required float Latitude { get; init; }

    public required float Longitude { get; init; }

    public required IList<string> Times { get; init; } = new string[0];

    public double Distance { get; private set; }

    public Stop(float userLatitude, float userLongitude)
    {
        Distance = GeoCalculator.GetDistance(userLatitude, userLongitude, Latitude, Longitude, 1, DistanceUnit.Meters);
    }

    public override string ToString()
    {
        var stringBuilder = new StringBuilder();

        stringBuilder.AppendLine($"{nameof(Id)} = {Id}");
        stringBuilder.AppendLine($"{nameof(Name)} = {Name}");
        stringBuilder.AppendLine($"{nameof(Latitude)} = {Latitude}");
        stringBuilder.AppendLine($"{nameof(Longitude)} = {Longitude}");

        stringBuilder.AppendLine($"{nameof(Times)} =");
        foreach (var time in Times)
        {
            stringBuilder.AppendLine($"{time}");
        }

        return stringBuilder.ToString();
    }
}
