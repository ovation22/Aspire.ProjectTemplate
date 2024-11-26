using Aspire.ProjectTemplate.Core.Entities;
using Microsoft.EntityFrameworkCore;

namespace Aspire.ProjectTemplate.Infrastructure.Persistence;

public class ProjectTemplateContext(DbContextOptions<ProjectTemplateContext> dbContextOptions)
    : DbContext(dbContextOptions)
{
    public virtual DbSet<WeatherForecast> WeatherForecasts => Set<WeatherForecast>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(ProjectTemplateContext).Assembly);

        SeedData(modelBuilder);

        base.OnModelCreating(modelBuilder);
    }

    private static void SeedData(ModelBuilder modelBuilder)
    {
        // Summaries and corresponding temperature ranges
        var summaries = new (string Summary, (int Min, int Max) TemperatureRange)[]
        {
            ("Freezing", (-20, 0)),
            ("Bracing", (0, 5)),
            ("Chilly", (5, 10)),
            ("Cool", (10, 15)),
            ("Mild", (15, 20)),
            ("Warm", (20, 25)),
            ("Balmy", (25, 30)),
            ("Hot", (30, 35)),
            ("Sweltering", (35, 40)),
            ("Scorching", (40, 45))
        };

        var forecasts = new List<WeatherForecast>();
        var random = new Random();

        var id = 1;

        foreach(var (summary, temperatureRange) in summaries)
        {
            for(var i = 0;i < 5;i++) // Generating 5 entries per summary for demonstration
            {
                forecasts.Add(new WeatherForecast
                {
                    Id = id++,
                    Date = DateOnly.FromDateTime(DateTime.Now.AddDays(-i)),
                    TemperatureC = random.Next(temperatureRange.Min, temperatureRange.Max),
                    Summary = summary
                });
            }
        }

        // Seed WeatherForecasts data
        modelBuilder.Entity<WeatherForecast>().HasData(forecasts);
    }
}
