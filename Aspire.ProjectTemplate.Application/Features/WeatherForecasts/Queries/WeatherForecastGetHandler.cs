using System.Linq.Expressions;
using Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Queries;
using Aspire.ProjectTemplate.Core.Interfaces.Persistence;
using Aspire.ProjectTemplate.Core.Models;
using Aspire.ProjectTemplate.Core.Specifications.WeatherForecasts;
using AutoMapper;
using MediatR;

namespace Aspire.ProjectTemplate.Application.Features.WeatherForecasts.Queries;

public class WeatherForecastGetHandler(IQueryRepository repository, IMapper mapper)
    : IRequestHandler<WeatherForecastsGet, WeatherForecastResponse>
{
    public async Task<WeatherForecastResponse> Handle(WeatherForecastsGet request, CancellationToken cancellationToken)
    {
        // Example of a specification that takes in a mapper.
        var spec = new WeatherForecastGetAllSpecification(mapper);
        Expression<Func<Core.Entities.WeatherForecast, bool>> x = x => x.Id == request.Id;
        var result = await repository.SingleOrDefaultAsync(x, cancellationToken);

        // Be aware that mapper.Map performs this operation in memory, not at the database.
        return mapper.Map<WeatherForecastResponse>(result);
    }
}
