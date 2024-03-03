namespace DistanceCalculator.Api;

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class Program
{
    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        var agency = await GetTtcAgency();
        app.MapGet("/nearest-stops", (float latitude, float longitude) =>
        {
            Console.WriteLine($"{nameof(latitude)}={latitude}");
            Console.WriteLine($"{nameof(longitude)}={longitude}");
            var nearestStops = agency.GetNearestStops(latitude, longitude).ToArray();
            foreach (var item in nearestStops)
            {
                Console.WriteLine(item.ToString());
            }

            return nearestStops;
        })
        .WithName("GetNearestStops")
        .WithOpenApi();

        var summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        app.MapGet("/weatherforecast", () =>
        {
            var forecast = Enumerable.Range(1, 5).Select(index =>
                new WeatherForecast
                (
                    DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
                    Random.Shared.Next(-20, 55),
                    summaries[Random.Shared.Next(summaries.Length)]
                ))
                .ToArray();
            return forecast;
        })
        .WithName("GetWeatherForecast")
        .WithOpenApi();

        app.Run();
    }

    private static async Task<Common.Models.Agency> GetTtcAgency()
    {
        return await Common.Models.TtcAgency.GetAgency();
    }
}
