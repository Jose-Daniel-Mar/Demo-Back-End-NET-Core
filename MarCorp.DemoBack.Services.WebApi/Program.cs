using MarCorp.DemoBack.Services.WebApi.Modules;
using MarCorp.DemoBack.Services.WebApi.Modules.Authentication;
using MarCorp.DemoBack.Services.WebApi.Modules.Injection;
using MarCorp.DemoBack.Services.WebApi.Modules.Swagger;
using MarCorp.DemoBack.Services.WebApi.Modules.Validator;

var builder = WebApplication.CreateBuilder(args);
var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json", optional: true, reloadOnChange: true).Build();

builder.Services.AddControllers();
builder.Services.AddOpenApi();
builder.Services.AddSwagger();
builder.Services.AddAuthentication(configuration);
builder.Services.AddMapper();
builder.Services.AddInjection(configuration);
builder.Services.AddValidator();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.MapOpenApi();
}

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "MarCorp API v1");
    c.RoutePrefix = string.Empty; // Swagger en la raíz
});

app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
