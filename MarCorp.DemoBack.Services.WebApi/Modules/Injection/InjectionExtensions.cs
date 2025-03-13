using MarCorp.DemoBack.Support.Common;
using MarCorp.DemoBack.Support.Logging;

namespace MarCorp.DemoBack.Services.WebApi.Modules.Injection
{
    /// <summary>
    /// Provides extension methods for dependency injection.
    /// </summary>
    public static class InjectionExtensions
    {
        /// <summary>
        /// Adds the necessary services to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>
        /// <param name="configuration">The configuration to use for the services.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IConfiguration>(configuration);
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
