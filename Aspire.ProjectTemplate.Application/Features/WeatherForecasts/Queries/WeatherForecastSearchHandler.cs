using Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Queries;
using Aspire.ProjectTemplate.Core.Entities;
using Aspire.ProjectTemplate.Core.Interfaces.Persistence;
using Aspire.ProjectTemplate.Core.Models;
using Aspire.ProjectTemplate.Core.Specifications.WeatherForecasts;
using MediatR;

namespace Aspire.ProjectTemplate.Application.Features.WeatherForecasts.Queries;

public class WeatherForecastSearchHandler(IQueryRepository repository)
    : IRequestHandler<WeatherForecastsSearch, PagedList<WeatherForecastResponse>>
{
    public async Task<PagedList<WeatherForecastResponse>> Handle(WeatherForecastsSearch request, CancellationToken cancellationToken)
    {
        // Example of a paginated search that returns a paginated list of models.
        //var spec = new WeatherForecastSearchProjectionSpecification(request.PaginationRequest);
        //var result = await repository.ListAsync(spec, cancellationToken);

        var spec = new WeatherForecastSearchSpecification(request.PaginationRequest);
        var result = await repository.ListAsync<WeatherForecast, WeatherForecastResponse>(spec, cancellationToken);

        return result;
    }
}
