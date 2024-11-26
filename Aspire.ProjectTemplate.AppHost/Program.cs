using Projects;

var builder = DistributedApplication.CreateBuilder(args);

var password = builder.AddParameter("password", secret: true);

var sql = builder.AddSqlServer("sql", password)
    .WithDataVolume()
    .WithLifetime(ContainerLifetime.Persistent)
    .AddDatabase("ProjectTemplate");

var apiService = builder.AddProject<Aspire_ProjectTemplate_ApiService>("apiservice")
    .WithReference(sql)
    .WaitFor(sql);

builder.AddProject<Aspire_ProjectTemplate_Web>("projecttemplateweb")
    .WithExternalHttpEndpoints()
    .WithReference(apiService)
    .WaitFor(apiService);

builder.Build().Run();