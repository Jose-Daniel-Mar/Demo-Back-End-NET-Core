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
            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("NorthwindConnection"), tags: new[] { "database" })
                .AddRedis(configuration.GetConnectionString("RedisConnection"), tags: new[] { "cache" })
                .AddCheck<HealthCheckCustom>("HealthCheckCustom", tags: new[] { "custom" });
            services.AddHealthChecksUI().AddInMemoryStorage();

            return services;
        }
    }
}
