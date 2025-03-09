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

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();


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
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "MarCorp API v1");
        c.RoutePrefix = "swagger";
    });
}

app.UseWatchDogExceptionLogger();
app.UseHttpsRedirection();
app.UseCors("policyApiMarCorp");
app.UseRouting();
app.UseSession();
app.UseAuthentication();
app.UseAuthorization();
app.UseRateLimiter();
app.UseEndpoints(_ => { });
app.MapHealthChecksUI();
app.MapHealthChecks("/health", new Microsoft.AspNetCore.Diagnostics.HealthChecks.HealthCheckOptions
{
    Predicate = _ => true,
    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
});
app.UseWatchDog(conf =>
{
    conf.WatchPageUsername = builder.Configuration["WatchDog:WatchPageUsername"];
    conf.WatchPagePassword = builder.Configuration["WatchDog:WatchPagePassword"];
});
 
app.MapControllers();
app.Run(); 

/// <summary>
/// The main entry point for the application.
/// </summary>
public partial class Program { };