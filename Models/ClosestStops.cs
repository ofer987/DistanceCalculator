using Geolocation;

namespace DistanceCalculator.Models;

public class ClosestStops
{
    public required float Latitude { get; init; }

    public required float Longitude { get; init; }

    public double distanceTo(float latitude, float longitude)
    {
        return GeoCalculator.GetDistance(Latitude, Longitude, latitude, longitude, 1, DistanceUnit.Meters);
    }

    public IEnumerable<Stop> Get(IEnumerable<Stop> allStops)
    {
        return allStops
            .Where(item => item.Distance <= 500d)
            .OrderBy(item => item.Distance);
    }
}
