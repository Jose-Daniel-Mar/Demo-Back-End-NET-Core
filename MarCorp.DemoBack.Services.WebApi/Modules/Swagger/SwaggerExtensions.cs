using Microsoft.OpenApi.Models;
using System.Reflection;

namespace MarCorp.DemoBack.Services.WebApi.Modules.Swagger
{
    public static class SwaggerExtensions
    {
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo
                {
                    Version = "v1",
                    Title = "MarCorp Technology Services API",
                    Description = "Example ASP.NET Core Web API",
                    TermsOfService = new Uri("https://example.com/terms"),
                    Contact = new OpenApiContact
                    {
                        Name = "Daniel Mar",
                        Email = "daniel.mar.iest.edu.mx",
                        Url = new Uri("https://example.com/contacto")
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                        Url = new Uri("https://example.com/licencia")
                    }
                });
                // Set the comments path for the Swagger JSON and UI.
                var xmlFile = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                c.IncludeXmlComments(xmlPath);
                c.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
                {
                    Description = "Authorization by API key.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.ApiKey,
                    Name = "Authorization"
                });

                c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Authorization"
                            }
                        },
                        new string[] { }
                    }
                });
            });
            return services;
        }
    }
}
