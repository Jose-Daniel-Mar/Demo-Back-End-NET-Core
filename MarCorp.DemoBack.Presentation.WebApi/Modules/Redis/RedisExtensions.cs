namespace MarCorp.DemoBack.Services.WebApi.Modules.Redis
{
    /// <summary>
    /// Provides extension methods for adding Redis cache services.
    /// </summary>
    public static class RedisExtensions
    {
        /// <summary>
        /// Adds Redis cache services to the specified IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <param name="configuration">The configuration to use for the Redis connection.</param>
        /// <returns>The IServiceCollection with the added services.</returns>
        public static IServiceCollection AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisCache(options =>
            {
                options.Configuration = configuration.GetConnectionString("RedisConnection");
            });
            return services;
        }
    }
}
