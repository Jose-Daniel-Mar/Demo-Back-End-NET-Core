using HealthChecks.UI.Client;
using MarCorp.DemoBack.Persistence;
using MarCorp.DemoBack.Application.UseCases;
using MarCorp.DemoBack.Services.WebApi.Modules.Authentication;
using MarCorp.DemoBack.Services.WebApi.Modules.Cors;
using MarCorp.DemoBack.Services.WebApi.Modules.HealthCheck;
using MarCorp.DemoBack.Services.WebApi.Modules.Injection;
using MarCorp.DemoBack.Services.WebApi.Modules.RateLimiter;
using MarCorp.DemoBack.Services.WebApi.Modules.Redis;
using MarCorp.DemoBack.Services.WebApi.Modules.Swagger;
using MarCorp.DemoBack.Services.WebApi.Modules.Watch;
using WatchDog;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using MarCorp.DemoBack.Infrastructure;

// Configuración inicial
var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

// Registro de servicios y dependencias
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddFCors(configuration);
builder.Services.AddPersistenceServices(configuration);
builder.Services.AddInfrastructureServices();
builder.Services.AddApplicationServices();
builder.Services.AddSwagger();
builder.Services.AddAuthentication(configuration);
builder.Services.AddInjection(configuration);
builder.Services.AddHealthCheck(configuration);
builder.Services.AddWatchDog(configuration);
// DESCOMENTAR PARA USAR REDIS
//builder.Services.AddRedisCache(configuration);
builder.Services.AddRatelimiting(configuration);

var app = builder.Build();

// Herramientas de desarrollo
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MarCorp API v1");
        c.EnablePersistAuthorization();
        c.RoutePrefix = "swagger";
    });
}

// Routing
app.UseRouting();

// Middleware de monitoreo
app.UseWatchDogExceptionLogger();

// Seguridad y protocolos
app.UseHttpsRedirection();

// Políticas CORS
app.UseCors("policyApiMarCorp");

// Limitación de tasa de peticiones
app.UseRateLimiter();

// Identity
app.UseAuthentication();

// Policies/Roles
app.UseAuthorization(); 

app.MapControllers();

// Health Checks Configuration
app.MapHealthChecksUI();
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,  // Incluir todos los checks
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// WatchDog (monitoreo en tiempo real)
app.UseWatchDog(conf => {
    conf.WatchPageUsername = builder.Configuration["WatchDog:WatchPageUsername"];
    conf.WatchPagePassword = builder.Configuration["WatchDog:WatchPagePassword"];
});

app.Run();

/// <summary>
/// The main entry point for the application.
/// Nota: Esta clase partial es requerida para integración con tests.
/// </summary>
public partial class Program { };