using System.Text;

using Geolocation;

namespace DistanceCalculator.Models;

public class Stop
{
    public required Line Line { get; init; }

    public required int Id { get; init; }

    public required string Name { get; init; }

    public float Latitude { get; private set; }

    public float Longitude { get; private set; }

    public double Distance { get; private set; }

    public Stop(float latitude, float longitude, float userLatitude, float userLongitude)
    {
        Latitude = latitude;
        Longitude = longitude;
        Distance = GeoCalculator.GetDistance(userLatitude, userLongitude, Latitude, Longitude, 1, DistanceUnit.Meters);
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
