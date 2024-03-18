namespace DistanceCalculator.Common.Models;

public class TtcAgency : Agency
{
    public async static Task<Agency> GetAgency()
    {
        var agency = new TtcAgency();

        IEnumerable<TtcModels.RouteInformation> routes;
        try
        {
            routes = await RestApi.Ttc.GetRoutes();
        }
        catch (HttpRequestException exception)
        {
            Console.WriteLine("Failed to get the TTC's routes");
            Console.WriteLine(exception);

            return new NullAgency();
        }

        var adapter = new Adapters.TtcModelAdapter();
        var routeIds = new List<int>();
        foreach (var route in routes)
        {
            if (Int32.TryParse(route.ShortName, out var id))
            {
                routeIds.Add(id);
            }
        }
        foreach (var id in routeIds.Take(10))
        {
            TtcModels.Route route;
            try
            {
                route = await RestApi.Ttc.GetRoute(id);
            }
            catch (HttpRequestException exception)
            {
                Console.WriteLine($"Failed to get a route for {id}");
                Console.WriteLine(exception);

                continue;
            }

            var lines = adapter.CreateLines(route);
            foreach (var line in lines)
            {
                agency.Lines.Add(line);
            }
        }

        return agency;
    }
}
