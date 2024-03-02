namespace DistanceCalculator.Adapters;

using DistanceCalculator.Extensions;

public static class ModelAdapter
{
    public static IEnumerable<Models.Line> CreateLines(Route route, float latitude, float longitude)
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
                case 700:
                default:
                    line = new Models.BusLine
                    {
                        Id = route.Information.ShortName,
                        Name = branch.Direction.BranchName,
                    };
                    break;
            }

            if (line.Name.IsEmpty())
            {
                continue;
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
}
