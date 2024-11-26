using Ardalis.Specification;
using Aspire.ProjectTemplate.Core.Entities;
using AutoMapper;

namespace Aspire.ProjectTemplate.Core.Specifications.WeatherForecasts;

public sealed class WeatherForecastGetAllSpecification : Specification<WeatherForecast, Models.WeatherForecastResponse>
{
    public WeatherForecastGetAllSpecification()
    {
        Query.Select(wf =>
            new Models.WeatherForecastResponse
            {
                Id = wf.Id, Date = wf.Date, Summary = wf.Summary, TemperatureC = wf.TemperatureC,
            });
    }

    /// <summary>
    /// Be aware that mapper.Map performs this operation in memory, not at the database.
    /// </summary>
    /// <param name="mapper"></param>
    public WeatherForecastGetAllSpecification(IMapper mapper)
    {
        Query.Select(wf => mapper.Map<Models.WeatherForecastResponse>(wf));
    }
}
