using Aspire.ProjectTemplate.Core.Entities;
using Aspire.ProjectTemplate.Core.Models;

namespace Aspire.ProjectTemplate.Core.Specifications.WeatherForecasts;

public sealed class
    WeatherForecastSearchSpecification : PaginatedSpecification<WeatherForecast>
{
    private static readonly Dictionary<string, string> _propertyMappings = new(StringComparer.OrdinalIgnoreCase)
    {
        { "Temp", "TemperatureC" },
    };

    /// <summary>
    /// Initializes a new instance of the <see cref="WeatherForecastSearchProjectionSpecification"/> class with pagination request.
    /// </summary>
    /// <param name="request">The pagination request.</param>
    public WeatherForecastSearchSpecification(PaginationRequest request) : base(request.Page, request.Size)
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
    }
}
