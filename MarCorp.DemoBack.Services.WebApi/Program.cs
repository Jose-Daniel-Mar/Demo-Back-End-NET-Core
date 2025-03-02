using HealthChecks.UI.Client;
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
builder.Services.AddOpenApi();
builder.Services.AddFCors(configuration);
builder.Services.AddSwagger();
builder.Services.AddAuthentication(configuration);
builder.Services.AddMapper();
builder.Services.AddInjection(configuration);
builder.Services.AddValidator();
builder.Services.AddHealthCheck(configuration);
builder.Services.AddWatchDog(configuration);
builder.Services.AddRedisCache(configuration);
builder.Services.AddRatelimiting(configuration);

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
        c.RoutePrefix = string.Empty; // Swagger en la ra�z
    });
}

app.UseWatchDogExceptionLogger();
app.UseCors("policyApiMarCorp");
app.UseRouting();
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