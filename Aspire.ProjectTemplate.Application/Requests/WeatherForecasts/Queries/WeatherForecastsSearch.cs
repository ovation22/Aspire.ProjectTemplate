using Aspire.ProjectTemplate.Core.Models;

namespace Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Queries;

public class WeatherForecastsSearch : MediatR.IRequest<PagedList<WeatherForecastResponse>>
{
    public PaginationRequest PaginationRequest { get; set; } = default!;
}
