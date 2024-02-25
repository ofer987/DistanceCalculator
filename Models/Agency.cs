namespace DistanceCalculator.Models;

public class Agency
{
    public required string Name { get; init; }

    // public IList<Route> Routes { get; } = new List<Route>();

    public IList<Models.Line> Subways { get; private set; }
    public IList<Models.Line> Buses { get; private set; }

    public Agency()
    {
        Subways = new List<Models.Line>();
        Buses = new List<Models.Line>();
    }
}
