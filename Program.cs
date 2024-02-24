using System.Net;
using System.Net.Http.Headers;

using System.Text.Json;

// See https://aka.ms/new-console-template for more information
// Console.WriteLine("Hello, World!");
//
// var latitudeOne = -79.411065f;
// var latitudeTwo = -79.407124f;
//
// var longitudeOne = 43.666061f;
// var longitudeTwo = 43.674816f;
//
// var length = Math.Pow(latitudeOne - latitudeTwo, 2);
// var width = Math.Pow(longitudeOne - longitudeTwo, 2);
//
// var result = Math.Sqrt(length + width);
//
// Console.WriteLine(result);

var id = args[0] ?? "7";
var uri = $"https://www.ttc.ca/ttcapi/routedetail/get?id={id}";

using var client = new HttpClient();
using var request = new HttpRequestMessage(HttpMethod.Get, uri);
request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

try
{
    Console.WriteLine(uri);
    using var response = await client.SendAsync(request);
    if (response.StatusCode != HttpStatusCode.OK)
    {
        return 1;
    }


    var result = await response.Content.ReadAsStringAsync() ?? string.Empty;
    Console.WriteLine(result);

    var jsonResult = JsonSerializer.Deserialize<Branch>(result);

    Console.WriteLine(jsonResult);
}
catch (HttpRequestException exception)
{
    Console.WriteLine($"Failed to access {uri}");
    Console.WriteLine(exception);

    return 1;
}

return 0;
