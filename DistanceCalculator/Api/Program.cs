namespace DistanceCalculator.Api;

record WeatherForecast(DateOnly Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public class Program
{
    private const string DEVELOPMENT_POLICY = "DEVELOPMENT_POLICY";
    private const string PRODUCTION_POLICY = "PRODUCTION_POLICY";

    public async static Task Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.Services.AddCors(options =>
        {
            options.AddPolicy(
                DEVELOPMENT_POLICY,
                builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4173", "http://localhost:5173")
                        .WithMethods("GET")
                        .WithHeaders("Content-Type");
                });

            options.AddPolicy(
                PRODUCTION_POLICY,
                builder =>
                {
                    builder
                        .WithOrigins("https://transit.ofer.to")
                        .WithMethods("GET")
                        .WithHeaders("Content-Type");
                });
        });

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
            app.UseCors(DEVELOPMENT_POLICY);
        }
        else
        {
            app.UseCors(PRODUCTION_POLICY);
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
