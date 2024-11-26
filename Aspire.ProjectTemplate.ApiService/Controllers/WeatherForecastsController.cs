using System.Net;
using Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Commands;
using Aspire.ProjectTemplate.Application.Requests.WeatherForecasts.Queries;
using Aspire.ProjectTemplate.Core.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Aspire.ProjectTemplate.ApiService.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastsController(IMediator mediator)
    : ControllerBase
{
    /// <summary>
    /// Returns the Weather Forecast with the specified id.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /weatherforecasts/1
    /// 
    /// </remarks>
    /// <param name="id">The Id of the Weather Forecast to retrieve</param>
    /// <returns>The WeatherForecast object with the specified Id</returns>
    /// <response code="200">Returns the WeatherForecast object</response>
    /// <response code="204">Returns no content if the WeatherForecast with the specified Id is not found</response>
    [HttpGet("{id}", Name = "WeatherForecastsGet")]
    [ProducesResponseType(typeof(WeatherForecastResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    public async Task<IActionResult> Get([FromRoute] long id)
    {
        var response = await mediator.Send(new WeatherForecastsGet { Id = id });

        if(response == null)
        {
            return NoContent();
        }

        return Ok(response);
    }

    /// <summary>
    /// Filters the existing Weather Forecasts based on the provided values and pagination details.
    /// </summary>
    /// <remarks>
    /// Sample request:
    /// 
    ///     GET /weatherforecasts/?operator=or&amp;page=1&amp;size=15&amp;sortby=temp&amp;direction=asc&amp;filters[summary].operator=eq&amp;filters[summary].value=balmy&amp;filters[temp].operator=Gt&amp;filters[temp].value=2
    /// 
    /// </remarks>
    /// <param name="request">The pagination and filter request</param>
    /// <returns>A paged list of WeatherForecast objects</returns>
    /// <response code="200">Returns the paginated list of WeatherForecast objects</response>
    [HttpGet(Name = "WeatherForecastsSearch")]
    [ProducesResponseType(typeof(PagedList<WeatherForecastResponse>), (int)HttpStatusCode.OK)]
    public async Task<OkObjectResult> Search([FromQuery] PaginationRequest request)
    {
        var response = await mediator.Send(new WeatherForecastsSearch { PaginationRequest = request });

        return Ok(response);
    }

    /// <summary>
    /// Creates a new Weather Forecast.
    /// </summary>
    /// <param name="request">The Weather Forecast details</param>
    /// <returns>The created Weather Forecast</returns>
    /// <response code="201">Returns the newly created Weather Forecast</response>
    [HttpPost]
    [ProducesResponseType(typeof(WeatherForecastResponse), (int)HttpStatusCode.Created)]
    public async Task<IActionResult> Post([FromBody] WeatherForecastCreate request)
    {
        var response = await mediator.Send(request);

        return CreatedAtAction(nameof(Get), new { id = response.Id }, response);
    }

    /// <summary>
    /// Updates the Weather Forecast with the specified id.
    /// </summary>
    /// <param name="id">The Id of the Weather Forecast to update</param>
    /// <param name="request">The updated Weather Forecast details</param>
    /// <returns>The updated Weather Forecast</returns>
    [HttpPut("{id}")]
    [ProducesResponseType(typeof(WeatherForecastResponse), (int)HttpStatusCode.OK)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    [ProducesResponseType((int)HttpStatusCode.BadRequest)]
    public async Task<IActionResult> Put([FromRoute] int id, [FromBody] WeatherForecastUpdateRequest request)
    {
        var response = await mediator.Send(new WeatherForecastUpdate
        {
            Id = id, TemperatureC = request.TemperatureC, Summary = request.Summary
        });

        return Ok(response);
    }

    /// <summary>
    /// Deletes the Weather Forecast with the specified id.
    /// </summary>
    /// <param name="id">The Id of the Weather Forecast to delete</param>
    /// <response code="204">No content if the WeatherForecast is successfully deleted</response>
    /// <response code="404">If the WeatherForecast with the specified ID is not found</response>
    [HttpDelete("{id}")]
    [ProducesResponseType((int)HttpStatusCode.NoContent)]
    [ProducesResponseType((int)HttpStatusCode.NotFound)]
    public async Task<IActionResult> Delete([FromRoute] long id)
    {
        await mediator.Send(new WeatherForecastDelete { Id = id });

        return NoContent();
    }
}
