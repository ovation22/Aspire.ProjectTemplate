using Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Commands;
using Aspire.ProjectTemplate.Core.Entities;
using Aspire.ProjectTemplate.Core.Interfaces.Persistence;
using MediatR;

namespace Aspire.ProjectTemplate.Application.Features.WeatherForecasts.Commands;

public class WeatherForecastDeleteHandler(IRepository repository) : IRequestHandler<WeatherForecastDelete>
{
    public async Task Handle(WeatherForecastDelete request, CancellationToken cancellationToken)
    { 
        var weatherForecast = await repository.FindAsync<WeatherForecast>(request.Id, cancellationToken);

        if(weatherForecast == null)
        {
            throw new KeyNotFoundException($"WeatherForecast with Id {request.Id} not found.");
        }

        await repository.DeleteAsync(weatherForecast, cancellationToken);
    }
}
