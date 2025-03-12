using HealthChecks.UI.Client;
using MarCorp.DemoBack.Persistence;
using MarCorp.DemoBack.Application.UseCases;
using MarCorp.DemoBack.Services.WebApi.Modules;
using MarCorp.DemoBack.Services.WebApi.Modules.Authentication;
using MarCorp.DemoBack.Services.WebApi.Modules.Cors;
using MarCorp.DemoBack.Services.WebApi.Modules.HealthCheck;
using MarCorp.DemoBack.Services.WebApi.Modules.Injection;
using MarCorp.DemoBack.Services.WebApi.Modules.RateLimiter;
using MarCorp.DemoBack.Services.WebApi.Modules.Redis;
using MarCorp.DemoBack.Services.WebApi.Modules.Swagger;
using MarCorp.DemoBack.Services.WebApi.Modules.Validator;
using MarCorp.DemoBack.Services.WebApi.Modules.Watch;
using WatchDog;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

// 1. Configuración inicial ---------------------------------------------------
var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

// 2. Registro de servicios y dependencias --------------------------------------------------
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddOpenApi();
builder.Services.AddFCors(configuration);
builder.Services.AddPersistenceServices();
builder.Services.AddApplicationServices();
builder.Services.AddSwagger();
builder.Services.AddAuthentication(configuration);
builder.Services.AddMapper();
builder.Services.AddInjection(configuration);
builder.Services.AddValidator();
builder.Services.AddHealthCheck(configuration);
builder.Services.AddWatchDog(configuration);
builder.Services.AddRedisCache(configuration);
builder.Services.AddRatelimiting(configuration);
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(20);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
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

// Seguridad y protocolos
app.UseHttpsRedirection();  // Redirección HTTP a HTTPS

// Políticas CORS (debe estar después de Routing y antes de Auth)
app.UseCors("policyApiMarCorp");

// Autenticación y autorización (orden crítico)
app.UseRouting();
app.UseSession();           // Sessions middleware

// WatchDog (monitoreo en tiempo real)
app.UseWatchDog(conf => {
    conf.WatchPageUsername = builder.Configuration["WatchDog:WatchPageUsername"];
    conf.WatchPagePassword = builder.Configuration["WatchDog:WatchPagePassword"];
});
app.Use(async (context, next) =>
{
    if (context.Request.Path.StartsWithSegments("/wtchdlogger") ||
        context.Request.Path.StartsWithSegments("/WTCHDwatchpage"))
    {
        // Para rutas de WatchDog, saltarse la autenticación JWT
        await next();
    }
    else
    {
        // Para otras rutas, aplicar autenticación normal
        context.Request.Headers.Authorization = context.Request.Headers.Authorization;
        await next();
    }
});

// Identity
app.UseAuthentication();

// Policies/Roles
app.UseAuthorization(); 

// Limitación de tasa de peticiones
app.UseRateLimiter();

// Health Checks Configuration
app.MapHealthChecksUI();  // UI de monitoreo
app.MapHealthChecks("/health", new HealthCheckOptions
{
    Predicate = _ => true,  // Incluir todos los checks
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});

// Endpoints finales
app.MapControllers();

app.Run();

/// <summary>
/// The main entry point for the application.
/// Nota: Esta clase partial es requerida para integración con tests.
/// </summary>
public partial class Program { };