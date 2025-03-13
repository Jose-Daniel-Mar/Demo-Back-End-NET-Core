using MarCorp.DemoBack.Application.Interface.Persistence;
using MarCorp.DemoBack.Persistence.Contexts;
using MarCorp.DemoBack.Persistence.Repositories;
using Microsoft.Extensions.DependencyInjection;

namespace MarCorp.DemoBack.Persistence
{
    /// <summary>
    /// Provides extension methods for persistence services.
    /// </summary>
    public static class ConfigureServices
    {
        /// <summary>
        /// Adds the necessary services to the IServiceCollection.
        /// </summary>
        /// <param name="services">The IServiceCollection to add services to.</param>"
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services)
        {
            services.AddSingleton<IDapperContext, DapperContext>();
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            
            return services;
        }
    }
}
