using Microsoft.AspNetCore.RateLimiting;

namespace MarCorp.DemoBack.Services.WebApi.Modules.RateLimiter
{
    /// <summary>
    /// Extension methods for adding rate limiting services to the IServiceCollection.
    /// </summary>
    public static class RateLimiterExtensions
    {
        /// <summary>
        /// Adds rate limiting services to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add the services to.</param>
        /// <param name="configuration">The configuration to use for rate limiting settings.</param>
        /// <returns>The IServiceCollection with the rate limiting services added.</returns>
        public static IServiceCollection AddRatelimiting(this IServiceCollection services, IConfiguration configuration)
        {
            var fixedWindowPolicy = "fixedWindow";
            services.AddRateLimiter(configureOptions =>
            {
                configureOptions.AddFixedWindowLimiter(policyName: fixedWindowPolicy, fixedWindow =>
                {
                    fixedWindow.PermitLimit = int.Parse(configuration["RateLimiting:PermitLimit"]);
                    fixedWindow.Window = TimeSpan.FromSeconds(int.Parse(configuration["RateLimiting:Window"]));
                    fixedWindow.QueueProcessingOrder = System.Threading.RateLimiting.QueueProcessingOrder.OldestFirst;
                    fixedWindow.QueueLimit = int.Parse(configuration["RateLimiting:QueueLimit"]);
                });
                configureOptions.RejectionStatusCode = StatusCodes.Status429TooManyRequests;
            });

            return services;
        }
    }
}
