namespace Aspire.ProjectTemplate.Core.Entities;

public class WeatherForecast
{
    public long Id { get; set; } = default!;

    public DateOnly Date { get; set; }

    public int TemperatureC { get; set; }

    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);

    public string? Summary { get; set; }
}
