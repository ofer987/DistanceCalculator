namespace DistanceCalculator.Common.Adapters;

// using DistanceCalculator.Common.Extensions;

public class YrtModelAdapter
{
    // public YrtModelAdapter() : base("YRT/VIVA") { }

    public IDictionary<int, YrtModels.Route> Routes { get; private set; } = new Dictionary<int, YrtModels.Route>();

    public IDictionary<int, YrtModels.Stop> Stops { get; private set; } = new Dictionary<int, YrtModels.Stop>();

    public IList<YrtModels.StopTime> StopTimes { get; private set; } = new List<YrtModels.StopTime>();

    public IList<YrtModels.Trip> Trips { get; private set; } = new List<YrtModels.Trip>();

    public YrtModelAdapter(IEnumerable<YrtModels.Route> routes, IEnumerable<YrtModels.Stop> stops, IEnumerable<YrtModels.StopTime> stopTimes, IEnumerable<YrtModels.Trip> trips)
    {
        Routes = routes.ToDictionary(item => item.Id, item => item);
        Stops = stops.ToDictionary(item => item.Id, item => item);
        StopTimes = stopTimes.ToList();
        Trips = trips.ToList();
    }

    public IList<Models.Line> GetLines()
    {
        var lines = new List<Models.Line>();
        foreach (var trip in Trips)
        {
            var routeId = trip.RouteId;

            if (!Routes.TryGetValue(routeId, out var route) || route is null)
            {
                continue;
            }

            var line = new Models.Line
            {
                Id = route.Id,
                Agency = "YRT / VIVA",
                // Name = route.GetName()
            };

            var stopTimes = StopTimes.Where(stopTime => stopTime.TripId == trip.TripId
                    && stopTime.StopId == stopTime.TripId);

            foreach (var stopTime in stopTimes)
            {
                if (!Stops.TryGetValue(stopTime.StopId, out var yrtStop) || yrtStop is null)
                {
                    continue;
                }

                var stop = new Models.Stop
                {
                    Name = trip.TripShortName,
                    Id = stopTime.StopId,
                    Latitude = yrtStop.Latitude,
                    Longitude = yrtStop.Longitude
                };

                line.Stops.Add(stop);
            }

            lines.Add(line);
        }

        return lines;
    }
}
