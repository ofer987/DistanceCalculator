// System.Environment.Get
// Envir
// 43.7645575,-79.4932447var userLongitude = 43.6587161f;
// var userLatitude = -79.4461468f;
namespace DistanceCalculator;

public class Program
{
    public static async Task<int> Main(string[] args)
    {
        const string LONGITUDE = "LONGITUDE";
        const string LATITUDE = "LATITUDE";

        const string DEFAULT_LONGITUDE = "43.6587161";
        const string DEFAULT_LATITUDE = " -79.4461468";
        var longitude = float.Parse(Environment.GetEnvironmentVariable(LONGITUDE) ?? DEFAULT_LONGITUDE);
        var latitude = float.Parse(Environment.GetEnvironmentVariable(LATITUDE) ?? DEFAULT_LATITUDE);
        var agency = DistanceCalculator.Adapters.ModelAdapter.CreateAgency();

        IEnumerable<RouteInformation> routes;
        try
        {
            routes = await DistanceCalculator.RestApi.Ttc.GetRoutes();
        }
        catch (HttpRequestException exception)
        {
            Console.WriteLine("Failed to get the TTC's routes");
            Console.WriteLine(exception);

            return 1;
        }

        var routeIds = new List<int>();
        foreach (var route in routes)
        {
            if (Int32.TryParse(route.ShortName, out var id))
            {
                routeIds.Add(id);
            }
        }
        foreach (var id in routeIds)
        {
            Route route;
            try
            {
                route = await DistanceCalculator.RestApi.Ttc.GetRoute(id);
            }
            catch (HttpRequestException exception)
            {
                Console.WriteLine($"Failed to get a route for {id}");
                Console.WriteLine(exception);

                continue;
            }

            var lines = DistanceCalculator.Adapters.ModelAdapter.CreateLines(agency, route, longitude, latitude);
            foreach (var line in lines)
            {
                agency.Lines.Add(line);
            }
        }

        var allLines = agency.Lines;
        var closestStops = allLines.SelectMany(line => line.Stops)
            .Where(stop => stop.Distance <= 5000d)
            .OrderBy(stop => stop.Distance)
            .GroupBy(stop => stop.Line.FullName)
            .Select(stop => stop.First());

        Console.WriteLine(agency);

        return 0;
    }

    private static float ConvertToCoordinate(string val)
    {
        return float.Parse(val);
    }
}
