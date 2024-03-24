namespace DistanceCalculator.Common.Adapters;

using DistanceCalculator.Common.TtcModels;
using DistanceCalculator.Common.Extensions;

public class TtcModelAdapter : ModelAdapter
{
    public TtcModelAdapter() : base("TTC") { }

    public override IEnumerable<Models.Line> CreateLines(Route route)
    {
        foreach (var branch in route.Branches)
        {
            Models.Line line;
            switch (route.Information.Type)
            {
                case 400:
                    line = new Models.SubwayLine
                    {
                        Agency = AgencyName,
                        Id = route.Information.ShortName,
                        Name = branch.Direction.HeadSign,
                    };
                    break;
                case 900:
                    line = new Models.StreetcarLine
                    {
                        Agency = AgencyName,
                        Id = route.Information.ShortName,
                        Name = branch.Direction.BranchName,
                    };
                    break;
                case 700:
                default:
                    line = new Models.BusLine
                    {
                        Agency = AgencyName,
                        Id = route.Information.ShortName,
                        Name = branch.Direction.BranchName,
                    };
                    break;
            }

            if (line.Name.IsEmpty())
            {
                continue;
            }

            var stopDictionary = new Dictionary<int, bool>();
            foreach (var stop in branch.Stops)
            {
                if (!stopDictionary.TryGetValue(stop.Id, out var _exists))
                {
                    var station = new Models.Stop
                    {
                        Line = line,
                        Id = stop.Id,
                        Name = stop.Name,
                        Latitude = stop.Latitude,
                        Longitude = stop.Longitude
                    };
                    stopDictionary.Add(stop.Id, true);
                    line.Stops.Add(station);
                }
            }

            yield return line;
        }
    }
}
