using Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Queries;
using Aspire.ProjectTemplate.Core.Interfaces.Persistence;
using Aspire.ProjectTemplate.Core.Models;
using Aspire.ProjectTemplate.Core.Specifications.WeatherForecasts;
using AutoMapper;
using MediatR;

namespace Aspire.ProjectTemplate.Application.Features.WeatherForecasts.Queries;

public class WeatherForecastGetAllHandler(IQueryRepository repository, IMapper mapper)
    : IRequestHandler<WeatherForecastsGetAll, List<WeatherForecastResponse>>
{
    public async Task<List<WeatherForecastResponse>> Handle(WeatherForecastsGetAll request, CancellationToken cancellationToken)
    {
        // Example of a specification that takes in a mapper.
        var spec = new WeatherForecastGetAllSpecification(mapper);
        var result = await repository.ListAsync(spec, cancellationToken);

        return result;
    }
}
