namespace DistanceCalculator.Models;

public class Agency
{
    public required string Name { get; init; }

    // public IList<Route> Routes { get; } = new List<Route>();

    public IList<Models.Line> Subways => new List<Models.Line>();
    public IList<Models.Line> Buses => new List<Models.Line>();
}
