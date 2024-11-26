using Aspire.ProjectTemplate.ApiService.Handlers;
using Aspire.ProjectTemplate.Application.Mappings;
using Aspire.ProjectTemplate.Core.Interfaces.Persistence;
using Aspire.ProjectTemplate.Infrastructure.Persistence;
using Aspire.ProjectTemplate.ServiceDefaults;
using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add service defaults & Aspire client integrations.
builder.AddServiceDefaults();

builder.Services.AddMediatR(config => { config.RegisterServicesFromAssemblyContaining<MappingProfile>(); });
builder.Services.AddAutoMapper(typeof(MappingProfile));

// Make routes globally lowercase.    
builder.Services.AddRouting(options =>
{
    options.LowercaseUrls = true;
    options.LowercaseQueryStrings = true;
});

//builder.AddSqlServerDbContext<ProjectTemplateContext>("sql");

var connectionString = builder.Configuration.GetConnectionString("ProjectTemplate");

builder.Services.AddDbContextPool<ProjectTemplateContext>(options =>
{
    options.UseSqlServer(connectionString);
});

builder.Services.AddScoped(typeof(IRepository), typeof(ProjectTemplateRepository));
builder.Services.AddScoped(typeof(IQueryRepository), typeof(ProjectTemplateRepository));
builder.Services.AddScoped(typeof(ICommandRepository), typeof(ProjectTemplateRepository));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAllOrigins",
        policyBuilder =>
        {
            policyBuilder.AllowAnyOrigin()  // Allow Blazor app
                .AllowAnyMethod()
                .AllowAnyHeader();
        });
});

builder.Services.AddControllers()
    .AddJsonOptions(options => { options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter()); });

// Add services to the container.
builder.Services.AddExceptionHandler<ExceptionHandler>();
builder.Services.AddProblemDetails();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

app.UseCors("AllowAllOrigins");

// Configure the HTTP request pipeline.
app.UseExceptionHandler();

if (app.Environment.IsDevelopment())
{
    EnsureDbCreated(app.Services);
    app.MapOpenApi();
}

app.UseRouting();

app.MapControllers();

app.MapDefaultEndpoints();

app.Run();

static void EnsureDbCreated(IServiceProvider appServices)
{
    using var db = appServices.CreateScope().ServiceProvider.GetRequiredService<ProjectTemplateContext>();

    db.Database.EnsureCreated();
}