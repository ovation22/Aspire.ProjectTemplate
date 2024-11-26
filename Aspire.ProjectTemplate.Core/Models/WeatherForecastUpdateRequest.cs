using MediatR;

namespace Aspire.ProjectTemplate.Core.Models;

public class WeatherForecastUpdateRequest : IRequest<WeatherForecastResponse>
{
    public int TemperatureC { get; set; }

    public string Summary { get; set; } = default!;
}
