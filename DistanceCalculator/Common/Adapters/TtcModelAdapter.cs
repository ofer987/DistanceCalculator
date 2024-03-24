namespace DistanceCalculator.Common.Adapters;

using DistanceCalculator.Common.TtcModels;
// using DistanceCalculator.Common.Extensions;

public class TtcModelAdapter : ModelAdapter
{
    public TtcModelAdapter() : base("TTC") { }

    public override IEnumerable<Models.Line> CreateLines(Route route)
    {
        Models.Line line;
        switch (route.Information.Type)
        {
            case 400:
                line = new Models.SubwayLine
                {
                    Agency = AgencyName,
                    Id = int.Parse(route.Information.ShortName)
                };
                break;
            case 900:
                line = new Models.StreetcarLine
                {
                    Agency = AgencyName,
                    Id = int.Parse(route.Information.ShortName),
                };
                break;
            case 700:
            default:
                line = new Models.BusLine
                {
                    Agency = AgencyName,
                    Id = int.Parse(route.Information.ShortName),
                };
                break;
        }
        // if (line.Name.IsEmpty())
        // {
        //     throw new Exception("Not Valid");
        // }

        // var destination = new Models.Destination(line, 
        // var stops = new List<Models.Stop>();
        foreach (var branch in route.Branches)
        {
            var destinationName = string.Empty;
            switch (line.Type)
            {
                case Models.LineTypes.Subway:
                    destinationName = branch.Direction.BranchName;
                    break;
                case Models.LineTypes.Streetcar:
                case Models.LineTypes.Bus:
                default:
                    destinationName = branch.Direction.HeadSign;
                    break;
            }

            var destination = new Models.Destination
            {
                Name = destinationName
            };

            foreach (var stop in branch.Stops)
            {
                var stopModel = new Models.Stop
                {
                    Id = stop.Id,
                    Name = stop.Name,
                    Latitude = stop.Latitude,
                    Longitude = stop.Longitude,
                };
                stopModel.Destinations.Add(destination);

                line.Stops.Add(stopModel);
            }
        }
        //         var stops = route.Branches
        //             .SelectMany(branch => branch.Stops)
        //             .Select(stop => new Models.Stop
        //             {
        //                 Id = stop.Id,
        //                 Name = stop.Name,
        //                 Latitude = stop.Latitude,
        //                 Longitude = stop.Longitude
        //             })
        //             .ToList();
        //
        //         foreach (var branch in route.Branches)
        //         {
        //             var destination = new Models.Destination(
        //         }
        //     }
        //     var direction = new Direction();
        //         foreach (var stop in branch.Stops)
        //         {
        //             var station = new Models.Stop
        //             {
        //                 Id = stop.Id,
        //                 Name = stop.Name,
        //                 Latitude = stop.Latitude,
        //                 Longitude = stop.Longitude
        //             };
        //
        //     var trip = new Models.Destination(line, station)
        //     {
        //         Name = stop.Name
        //     };
        //     station.Destinations.Add(trip);
        //
        //             line.Stops.Add(station);
        //         }
        //
        yield return line;
    }
}
