using Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Commands;
using Aspire.ProjectTemplate.Core.Interfaces.Persistence;
using Aspire.ProjectTemplate.Core.Models;
using AutoMapper;
using MediatR;

namespace Aspire.ProjectTemplate.Application.Features.WeatherForecasts.Commands;

public class WeatherForecastCreateHandler(IMapper mapper, IRepository repository)
    : IRequestHandler<WeatherForecastCreate, WeatherForecastResponse>
{
    public async Task<WeatherForecastResponse> Handle(WeatherForecastCreate request, CancellationToken cancellationToken)
    {
        var weatherForecast = new Core.Entities.WeatherForecast
        {
            Date = DateOnly.FromDateTime(request.Date),
            TemperatureC = request.TemperatureC,
            Summary = request.Summary
        };

        var result = await repository.CreateAsync(weatherForecast, cancellationToken);

        return mapper.Map<WeatherForecastResponse>(result);
    }
}
