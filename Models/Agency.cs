namespace DistanceCalculator.Models;

using System.Linq;

public abstract class Agency
{
    public static Agency INSTANCE = new NullAgency();

    public virtual string Name { get; private set; }

    public virtual IList<Models.Line> Lines { get; } = new List<Line>();

    public IEnumerable<Models.Line> Subways => Lines.Where(item => item.Type == LineTypes.Subway);
    public IEnumerable<Models.Line> Streetcars => Lines.Where(item => item.Type == LineTypes.Streetcar);
    public IEnumerable<Models.Line> Buses => Lines.Where(item => item.Type == LineTypes.Bus);

    protected Agency(string name)
    {
        Name = name;
    }
}
