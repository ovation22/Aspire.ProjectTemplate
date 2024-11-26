using Aspire.ProjectTemplate.Core.Models;

namespace Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Commands;

public class WeatherForecastUpdate : MediatR.IRequest<WeatherForecastResponse>
{
    public long Id { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; } = default!;
}
