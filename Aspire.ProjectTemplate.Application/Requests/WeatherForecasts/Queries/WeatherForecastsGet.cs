using Aspire.ProjectTemplate.Core.Models;

namespace Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Queries;

public class WeatherForecastsGet : MediatR.IRequest<WeatherForecastResponse>
{
    public long Id { get; set; }
}
