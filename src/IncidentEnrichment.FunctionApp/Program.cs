using IncidentEnrichment.FunctionApp.Clients;
using IncidentEnrichment.FunctionApp.Repositories;
using IncidentEnrichment.FunctionApp.Services;
using Microsoft.Azure.Functions.Worker.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

var builder = FunctionsApplication.CreateBuilder(args);

builder.ConfigureFunctionsWebApplication();

builder.Services
    .AddApplicationInsightsTelemetryWorkerService()
    .ConfigureFunctionsApplicationInsights();

builder.Services.AddScoped<IIncidentRepository, IncidentRepository>();
builder.Services.AddScoped<IEnrichmentService, EnrichmentService>();
builder.Services.AddHttpClient<IServiceNowClient, ServiceNowClient>(client =>
{
    var baseUrl = Environment.GetEnvironmentVariable("ServiceNowBaseUrl");
    if (!string.IsNullOrWhiteSpace(baseUrl))
    {
        client.BaseAddress = new Uri(baseUrl.TrimEnd('/'));
    }
    client.Timeout = TimeSpan.FromSeconds(30);
});

var host = builder.Build();
host.Run();
