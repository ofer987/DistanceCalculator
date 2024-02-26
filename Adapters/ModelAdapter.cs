namespace DistanceCalculator.Adapters;

public static class ModelAdapter
{
    public static string TransitAgency = "TTC";

    public static int[] SubwayLineIds = new[] { 1, 2, 3, 4 };

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
            Models.Line line;
            switch (route.Information.Type)
            {
                case 400:
                    line = new Models.SubwayLine
                    {
                        Id = route.Information.ShortName,
                        Name = branch.Direction.HeadSign,
                    };
                    break;
                case 900:
                    line = new Models.StreetcarLine
                    {
                        Id = route.Information.ShortName,
                        Name = branch.Direction.BranchName,
                    };
                    break;
                default:
                    line = new Models.BusLine
                    {
                        Id = route.Information.ShortName,
                        Name = branch.Direction.BranchName,
                    };
            }

            foreach (var stop in branch.Stops)
            {
                var station = new Models.Stop(stop.Latitude, stop.Longitude, latitude, longitude)
                {
                    Line = line,
                    Id = stop.Id,
                    Name = stop.Name
                };

                line.Stops.Add(station);
            }

            yield return line;
        }
    }

    public static IEnumerable<Models.Line> CreateSubwayLines(Models.Agency agency, Route route, float latitude, float longitude)
    {
        foreach (var branch in route.Branches)
        {
            var line = new Models.SubwayLine
            {
                Id = route.Information.ShortName,
                Name = branch.Direction.HeadSign,
            };

            foreach (var stop in branch.Stops)
            {
                var station = new Models.Stop(stop.Latitude, stop.Longitude, latitude, longitude)
                {
                    Line = line,
                    Id = stop.Id,
                    Name = stop.Name
                };

                line.Stops.Add(station);
            }

            yield return line;
        }
    }

    public static IEnumerable<Models.Line> CreateBusLines(Models.Agency agency, Route route, float latitude, float longitude)
    {
        foreach (var branch in route.Branches)
        {
            var busLine = new Models.BusLine
            {
                Id = route.Information.ShortName,
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
