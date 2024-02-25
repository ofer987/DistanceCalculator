namespace DistanceCalculator.Adapters;

public static class ModelAdapter
{
    public static string TransitAgency => "TTC";

    // public IList<Models.SubwayLine> Subways => new List<Models.SubwayLine>();
    // public IList<Models.BusLine> Buses => new List<Models.BusLine>();

    public static Models.Agency CreateAgency()
    {
        return new Models.Agency
        {
            Name = TransitAgency
        };
    }

    public static IEnumerable<Models.Line> CreateLines(Models.Agency agency, Route route, float latitude, float longitude)
    {
        foreach (var branch in route.Branches)
        {
            var busLine = new Models.BusLine
            {
                Id = branch.RouteId.ShortName,
                Name = branch.Direction.BranchName,
            };

            foreach (var stop in branch.Stops)
            {
                var busStop = new Models.Stop(stop.Latitude, stop.Longitude, latitude, longitude)
                {
                    Line = busLine,
                    Id = stop.Id,
                    Name = stop.Name
                };

                busLine.Stops.Add(busStop);
            }

            yield return busLine;
        }
    }
}
