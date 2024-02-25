var userLongitude = 43.6587161f;
var userLatitude = -79.4461468f;

var agency = DistanceCalculator.Adapters.ModelAdapter.CreateAgency();

foreach (var id in DistanceCalculator.Adapters.ModelAdapter.SubwayLineIds)
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

    var lines = DistanceCalculator.Adapters.ModelAdapter.CreateSubwayLines(agency, route, userLongitude, userLatitude);
    foreach (var line in lines)
    {
        agency.Subways.Add(line);
    }
}

IEnumerable<RouteInformation> busRoutes;
try
{
    busRoutes = await DistanceCalculator.RestApi.Ttc.GetRoutes();
}
catch (HttpRequestException exception)
{
    Console.WriteLine("Failed to get the TTC's routes");
    Console.WriteLine(exception);

    return 1;
}

var busRouteIds = new List<int>();
foreach (var route in busRoutes)
{
    if (Int32.TryParse(route.ShortName, out var id))
    {
        if (DistanceCalculator.Adapters.ModelAdapter.SubwayLineIds.Contains(id))
        {
            continue;
        }

        busRouteIds.Add(id);
    }
}
foreach (var id in busRouteIds.Take(10))
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

    var lines = DistanceCalculator.Adapters.ModelAdapter.CreateBusLines(agency, route, userLongitude, userLatitude);
    foreach (var line in lines)
    {
        agency.Buses.Add(line);
    }
}

var allLines = agency.Subways.Concat(agency.Buses);
var closestStops = allLines.SelectMany(line => line.Stops)
    .Where(stop => stop.Distance <= 5000d)
    .OrderBy(stop => stop.Distance)
    .GroupBy(stop => stop.Line.FullName)
    .Select(stop => stop.First());

Console.WriteLine(agency);

return 0;
