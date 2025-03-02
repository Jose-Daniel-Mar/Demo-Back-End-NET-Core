using MarCorp.DemoBack.Application.Interface;
using MarCorp.DemoBack.Application.Main;
using MarCorp.DemoBack.Data.Connections;
using MarCorp.DemoBack.Data.Interface;
using MarCorp.DemoBack.Data.Repository;
using MarCorp.DemoBack.Domain.Core;
using MarCorp.DemoBack.Domain.Interface;
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
            services.AddSingleton<IDapperContext, DapperContext>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));
            services.AddScoped<ICustomerApplication, CustomerApplication>();
            services.AddScoped<ICustomerDomain, CustomerDomain>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<IUsersDomain, UsersDomain>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<ICategoriesApplication, CategoriesApplication>();
            services.AddScoped<ICategoriesDomain, CategoriesDomain>();

            return services;
        }
    }
}
