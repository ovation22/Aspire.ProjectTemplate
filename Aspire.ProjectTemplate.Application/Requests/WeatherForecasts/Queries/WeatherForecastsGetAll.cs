using Aspire.ProjectTemplate.Core.Models;

namespace Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Queries;

public class WeatherForecastsGetAll : MediatR.IRequest<List<WeatherForecastResponse>>
{
}
