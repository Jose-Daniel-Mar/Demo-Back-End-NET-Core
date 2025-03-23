using MarCorp.DemoBack.Support.Common;
using Microsoft.Extensions.DependencyInjection;

namespace MarCorp.DemoBack.Services.WebApi.Modules.HealthCheck
{
    /// <summary>
    /// Extension methods for adding health checks to the service collection.
    /// </summary>
    public static class HealthCheckExtensions
    {
        /// <summary>
        /// Adds health check services to the specified service collection.
        /// </summary>
        /// <param name="services">The service collection to add the health check services to.</param>
        /// <param name="configuration">The configuration to use for the health check services.</param>
        /// <returns>The service collection with the health check services added.</returns>
        public static IServiceCollection AddHealthCheck(this IServiceCollection services, IConfiguration configuration)
        {
            var serviceProvider = services.BuildServiceProvider();
            var logger = serviceProvider.GetRequiredService<IAppLogger<RedisHealthCheck>>();

            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("NorthwindConnection"), tags: new[] { "database" })
                .AddCheck<HealthCheckCustom>("HealthCheckCustomRandom", tags: new[] { "custom" })
                .AddCheck("redis", new RedisHealthCheck(configuration, logger), tags: new[] { "cache" });
            services.AddHealthChecksUI()
                // SQL Server storage for production uncomment this line
                //.AddSqlServerStorage(configuration.GetConnectionString("NorthwindConnection"))

                // Memory storage for development
                .AddInMemoryStorage();
                
            return services;
        }
    }
}
