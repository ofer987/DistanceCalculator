namespace DistanceCalculator.Common.Models;

using System.Linq;

public abstract class Agency
{
    public static Agency INSTANCE = new NullAgency();

    public virtual IList<Models.Line> Lines { get; } = new List<Line>();

    public IEnumerable<Models.Line> Subways => Lines.Where(item => item.Type == LineTypes.Subway);
    public IEnumerable<Models.Line> Streetcars => Lines.Where(item => item.Type == LineTypes.Streetcar);
    public IEnumerable<Models.Line> Buses => Lines.Where(item => item.Type == LineTypes.Bus);

    public IEnumerable<Stop> GetNearestStops(float latitude, float longitude)
    {
        Console.WriteLine("in here");
        Console.WriteLine($"There are {Lines.ToList().Count}");

        var stops = Lines.SelectMany(line => line.Stops)
            .Select(stop => new { Distance = stop.GetDistance(latitude, longitude), Stop = stop })
            .Where(record => record.Distance <= 500d)
            .OrderBy(record => record.Distance)
            .Select(record => record.Stop)
            .DistinctBy(stop => stop.Id);

            // .Select(stop => stop.First());

        var lines = stops
            .SelectMany(stop => stop.Destinations)
            .Select(destination => destination.Line);

        return stops;
    }
}
