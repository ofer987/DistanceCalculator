namespace DistanceCalculator.Models;

public abstract class Line
{
    public required string Id { get; init; }

    public required string Name { get; init; }

    public required IList<Stop> Stops { get; init; }
}
