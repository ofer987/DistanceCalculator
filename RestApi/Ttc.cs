using System.Net;
using System.Net.Http.Headers;

using System.Text.Json;

namespace DistanceCalculator.RestApi;

public static class Ttc
{
    // Get Bus Lines
    // https://www.ttc.ca/ttcapi/routedetail/listroutes
    public static async Task<IEnumerable<RouteInformation>> GetRoutes()
    {
        var uri = "https://www.ttc.ca/ttcapi/routedetail/listroutes";

        return await GetEnumerableJson(uri);
    }

    public static async Task<Route> GetRoute(int id)
    {
        var uri = $"https://www.ttc.ca/ttcapi/routedetail/get?id={id}";

        return await GetJson<Route>(uri);
    }

    private static async Task<T> GetJson<T>(string uri) where T : class
    {
        try
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            Console.WriteLine(uri);
            using var response = await client.SendAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException($"Received {response.StatusCode} instead of {HttpStatusCode.OK}");
            }


            var body = await response.Content.ReadAsStringAsync() ?? string.Empty;
            //
            // if (string.IsNullOrWhiteSpace(result) || result is null)
            // {
            //     throw new HttpRequestException("Received an empty response");
            // }

            var result = JsonSerializer.Deserialize<T>(body);
            if (result is null)
            {
                throw new HttpRequestException($"Failed to deserialize to type {typeof(T)}");
            }

            return result;
        }
        catch (HttpRequestException exception)
        {
            Console.WriteLine($"Failed to access {uri}");
            Console.WriteLine(exception);

            throw;
        }
    }

    private static async Task<IEnumerable<RouteInformation>> GetEnumerableJson(string uri)
    {
        try
        {
            using var client = new HttpClient();
            using var request = new HttpRequestMessage(HttpMethod.Get, uri);
            request.Headers.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

            // Console.WriteLine(uri);
            using var response = await client.SendAsync(request);
            if (response.StatusCode != HttpStatusCode.OK)
            {
                throw new HttpRequestException($"Received {response.StatusCode} instead of {HttpStatusCode.OK}");
            }


            var body = await response.Content.ReadAsStringAsync() ?? string.Empty;
            //
            // if (string.IsNullOrWhiteSpace(result) || result is null)
            // {
            //     throw new HttpRequestException("Received an empty response");
            // }

            var result = JsonSerializer.Deserialize<IEnumerable<RouteInformation>>(body);
            if (result is null)
            {
                throw new HttpRequestException($"Failed to deserialize to type {typeof(IEnumerable<RouteInformation>)}");
            }

            return result;
        }
        catch (HttpRequestException exception)
        {
            Console.WriteLine($"Failed to access {uri}");
            Console.WriteLine(exception);

            throw;
        }
    }
}
