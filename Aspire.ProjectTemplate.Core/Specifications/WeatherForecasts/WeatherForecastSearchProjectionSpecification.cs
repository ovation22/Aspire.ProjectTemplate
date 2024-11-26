using Ardalis.Specification;
using Aspire.ProjectTemplate.Core.Entities;
using Aspire.ProjectTemplate.Core.Models;

namespace Aspire.ProjectTemplate.Core.Specifications.WeatherForecasts;

public sealed class
    WeatherForecastSearchProjectionSpecification : PaginatedSpecification<WeatherForecast, WeatherForecastResponse>
{
    private static readonly Dictionary<string, string> _propertyMappings = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Temp", "TemperatureC" },
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherForecastSearchProjectionSpecification"/> class with pagination request.
    /// </summary>
    /// <param name="request">The pagination request.</param>
    public WeatherForecastSearchProjectionSpecification(PaginationRequest request) : base(request.Page, request.Size)
    {
        if(request.Filters != null && request.Filters.Any())
        {
            if(request.Operator == LogicalOperator.And)
            {
                ApplyAndFilters(request.Filters, _propertyMappings);
            }
            else
            {
                ApplyOrFilters(request.Filters, _propertyMappings);
            }
        }

        if(!string.IsNullOrWhiteSpace(request.SortBy))
        {
            ApplySorting(request.SortBy, request.Direction, _propertyMappings);
        }

        // Projecting to the WeatherForecast model performs this operation at the database level.
        Query.Select(wf =>
            new WeatherForecastResponse
            {
                Id = wf.Id,
                Date = wf.Date,
                Summary = wf.Summary,
                TemperatureC = wf.TemperatureC,
            });
    }
}
