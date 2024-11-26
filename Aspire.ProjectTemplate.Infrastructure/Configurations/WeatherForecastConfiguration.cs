using Aspire.ProjectTemplate.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Aspire.ProjectTemplate.Infrastructure.Configurations;

public class WeatherForecastConfiguration : IEntityTypeConfiguration<WeatherForecast>
{
    public void Configure(EntityTypeBuilder<WeatherForecast> builder)
    {
        builder.HasKey(wf => wf.Id);

        builder.Property(wf => wf.Date)
            .IsRequired();

        builder.Property(wf => wf.TemperatureC)
            .IsRequired();

        builder.Property(wf => wf.Summary)
            .HasMaxLength(200); 
    }
}
