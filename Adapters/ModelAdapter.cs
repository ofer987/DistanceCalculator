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

    public static IEnumerable<Models.Line> CreateLines(Models.Agency agency, Branch branch, float latitude, float longitude)
    {
        foreach (var route in branch.Routes)
        {
            var busLine = new Models.BusLine
            {
                Id = route.Id.ToString(),
                Name = route.Direction.BranchName,
            };

            foreach (var stop in route.Stops)
            {
                var busStop = new Models.Stop(latitude, longitude)
                {
                    Id = stop.Id,
                    Name = stop.Name,
                    Latitude = stop.Latitude,
                    Longitude = stop.Longitude
                };

                busLine.Stops.Add(busStop);
            }

            yield return busLine;
        }
    }
}
