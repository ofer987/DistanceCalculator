namespace DistanceCalculator.Models;

public class Agency
{
    public required string Name { get; init; }

    public IList<Models.Line> Subways { get; } = new List<Models.Line>();
    public IList<Models.Line> Buses { get; } = new List<Models.Line>();
}
