namespace DistanceCalculator.Models;

public abstract class Line
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public string FullName => $"{Id} - {Name}";

    public IList<Stop> Stops { get; } = new List<Stop>();
}
