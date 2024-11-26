using Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Commands;
using Aspire.ProjectTemplate.Core.Entities;
using Aspire.ProjectTemplate.Core.Interfaces.Persistence;
using Aspire.ProjectTemplate.Core.Models;
using AutoMapper;
using MediatR;

namespace Aspire.ProjectTemplate.Application.Features.WeatherForecasts.Commands;

public class WeatherForecastUpdateHandler(IMapper mapper, IRepository repository)
    : IRequestHandler<WeatherForecastUpdate, WeatherForecastResponse>
{
    public async Task<WeatherForecastResponse> Handle(WeatherForecastUpdate request, CancellationToken cancellationToken)
    {
        var weatherForecast = await repository.FindAsync<WeatherForecast>(request.Id, cancellationToken);

        if(weatherForecast == null)
        {
            throw new KeyNotFoundException($"WeatherForecast with Id {request.Id} not found.");
        }

        weatherForecast.TemperatureC = request.TemperatureC;
        weatherForecast.Summary = request.Summary;

        var result = await repository.UpdateAsync(weatherForecast, cancellationToken);

        return mapper.Map<WeatherForecastResponse>(result);
    }
}
