namespace DistanceCalculator.Models;

using System.Linq;

public class Agency
{
    public required string Name { get; init; }

    public IList<Models.Line> Lines { get; } = new List<Models.Line>();

    public IEnumerable<Models.Line> Subways => Lines.Where(item => item.Type == LineTypes.Subway);
    public IEnumerable<Models.Line> Streetcars => Lines.Where(item => item.Type == LineTypes.Streetcar);
    public IEnumerable<Models.Line> Buses => Lines.Where(item => item.Type == LineTypes.Bus);
}
