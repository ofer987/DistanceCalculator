namespace DistanceCalculator.Common.Adapters;

using DistanceCalculator.Common.TtcModels;

public abstract class ModelAdapter
{
    public string AgencyName { get; private set; }

    protected ModelAdapter(string agencyName)
    {
        AgencyName = agencyName;
    }

    public abstract IEnumerable<Models.Line> CreateLines(Route route);
}
