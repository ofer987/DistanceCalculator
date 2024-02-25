﻿// using System.Net;
// using System.Net.Http.Headers;
//
// using System.Text.Json;

using System.Linq;

// using DistanceCalculator.RestApi;
// namespace DistanceCalculator;

// Get Subway Lines
// https://www.ttc.ca/ttcapi/routedetail/GetSubwayLines
// Get Bus Lines
// https://www.ttc.ca/ttcapi/routedetail/listroutes

// using DistanceCalculator.RestApi;
// 43.6587161,-79.4461468

var userLongitude = 43.6587161f;
var userLatitude = -79.4461468f;

var routeIds = new List<int>();
foreach (var route in (await DistanceCalculator.RestApi.Ttc.GetRoutes()))
{
    if (Int32.TryParse(route.ShortName, out var id))
    {
        routeIds.Add(id);
    }
};

var agency = DistanceCalculator.Adapters.ModelAdapter.CreateAgency();

foreach (var id in routeIds.Take(10))
{
    var branch = await DistanceCalculator.RestApi.Ttc.GetBranch(id);

    var lines = DistanceCalculator.Adapters.ModelAdapter.CreateLines(agency, branch, userLongitude, userLatitude);
    foreach (var line in lines)
    {
        agency.Buses.Add(line);
    }
}

var closestStops = agency.Buses.SelectMany(bus => bus.Stops)
    .Where(stop => stop.Distance <= 5000d)
    .OrderBy(stop => stop.Distance)
    .GroupBy(stop => stop.Line.Id)
    .Select(stop => stop.First());

Console.WriteLine(agency);

// var id = "7";
// if (args.Length >= 1)
// {
//     id = args[0];
// }
// var uri = $"https://www.ttc.ca/ttcapi/routedetail/get?id={id}";

// using var client = new HttpClient();
// using var request = new HttpRequestMessage(HttpMethod.Get, uri);
// request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
//
// // Get Subway Lines
// // https://www.ttc.ca/ttcapi/routedetail/GetSubwayLines
// // Get Bus Lines
// // https://www.ttc.ca/ttcapi/routedetail/listroutes
//
// await RestApi
//
// try
// {
//     Console.WriteLine(uri);
//     using var response = await client.SendAsync(request);
//     if (response.StatusCode != HttpStatusCode.OK)
//     {
//         return 1;
//     }
//
//
//     var result = await response.Content.ReadAsStringAsync() ?? string.Empty;
//     Console.WriteLine(result);
//
//     var jsonResult = JsonSerializer.Deserialize<Branch>(result);
//
//     Console.WriteLine(jsonResult);
// }
// catch (HttpRequestException exception)
// {
//     Console.WriteLine($"Failed to access {uri}");
//     Console.WriteLine(exception);
//
//     return 1;
// }
//
// return 0;
