using Aspire.ProjectTemplate.Core.Models;
using System.Net;
using System.Text.Json;
using Aspire.ProjectTemplate.Web.Converters;

namespace Aspire.ProjectTemplate.Web;

public class WeatherApiClient(HttpClient httpClient)
{
    public async Task<PagedList<WeatherForecastResponse>> GetWeatherAsync(PaginationRequest paginationRequest, CancellationToken cancellationToken = default)
    {
        try
        {
            using var response = await httpClient.GetAsync($"weatherforecasts?page={paginationRequest.Page}&size={paginationRequest.Size}&sortby={paginationRequest.SortBy}&direction={paginationRequest.Direction}");

            if (response.IsSuccessStatusCode)
            {
                var options = new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                };
                options.Converters.Add(new PagedListConverter<WeatherForecastResponse>());

                await using var stream = await response.Content.ReadAsStreamAsync();
                var paginatedForecasts = await JsonSerializer.DeserializeAsync<PagedList<WeatherForecastResponse>>(stream, options);

                return paginatedForecasts ?? new PagedList<WeatherForecastResponse>(Enumerable.Empty<WeatherForecastResponse>().AsQueryable(), 0, 1, 10);
            }

            throw response.StatusCode switch
            {
                HttpStatusCode.NotFound => new HttpRequestException("Weather forecasts not found."),
                HttpStatusCode.InternalServerError => new HttpRequestException("Server error while retrieving weather forecasts."),
                _ => new HttpRequestException($"Unexpected HTTP status code: {response.StatusCode}")
            };
        }
        catch (HttpRequestException httpEx)
        {
            //logger.LogError(httpEx, "HTTP request error while fetching weather forecasts.");
            throw;
        }
        catch (JsonException jsonEx)
        {
            //logger.LogError(jsonEx, "Error deserializing weather forecasts response.");
            throw new ApplicationException("Error processing server response.", jsonEx);
        }
        catch (Exception ex)
        {
            //logger.LogError(ex, "Unexpected error while fetching weather forecasts.");
            throw new ApplicationException("An unexpected error occurred while fetching weather forecasts.", ex);
        }
    }
}
