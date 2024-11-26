using Aspire.ProjectTemplate.Core.Models;
using MediatR;

namespace Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Commands;

public class WeatherForecastCreate : IRequest<WeatherForecastResponse>
{
    public DateTime Date { get; set; }
    public int TemperatureC { get; set; }
    public string Summary { get; set; } = default!;
}
