using MediatR;

namespace Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Commands;

public class WeatherForecastDelete : IRequest
{
    public long Id { get; set; }
}
