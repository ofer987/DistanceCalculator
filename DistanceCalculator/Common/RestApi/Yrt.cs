using System.Globalization;

using CsvHelper;

namespace DistanceCalculator.Common.RestApi;

public static class Yrt
{
    public static string RoutesPath = "routes.txt";
    public static string StopsPath = "stops.txt";
    public static string StopTimesPath = "stop_times.txt";
    public static string TripsPath = "trips.txt";

    // Maybe convert to generic using a GetRecord???????
    public static YrtModels.Route[] GetRoutes()
    {
        using var reader = new StreamReader(RoutesPath);
        using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csvReader.GetRecords<YrtModels.Route>().ToArray();
    }

    public static YrtModels.Stop[] GetStops()
    {
        using var reader = new StreamReader(StopsPath);
        using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csvReader.GetRecords<YrtModels.Stop>().ToArray();
    }
    public static YrtModels.StopTime[] GetStopTimes()
    {
        using var reader = new StreamReader(RoutesPath);
        using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csvReader.GetRecords<YrtModels.StopTime>().ToArray();
    }
    public static YrtModels.Trip[] GetTrips()
    {
        using var reader = new StreamReader(RoutesPath);
        using var csvReader = new CsvReader(reader, CultureInfo.InvariantCulture);

        return csvReader.GetRecords<YrtModels.Trip>().ToArray();
    }
}
