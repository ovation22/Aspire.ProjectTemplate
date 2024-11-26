using Aspire.ProjectTemplate.Core.Entities;
using Aspire.ProjectTemplate.Core.Models;
using AutoMapper;

namespace Aspire.ProjectTemplate.Application.Mappings;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<WeatherForecast, WeatherForecastResponse>();
    }
}
