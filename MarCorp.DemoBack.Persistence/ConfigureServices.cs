using MarCorp.DemoBack.Application.Interface.Persistence;
using MarCorp.DemoBack.Persistence.Contexts;
using MarCorp.DemoBack.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Persistence.Interceptors;

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
        /// <param name="configuration">The IConfiguration to use for configuration.</param>
        /// <returns>The updated IServiceCollection.</returns>
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IDapperContext, DapperContext>();
            services.AddScoped<AuditableEntitySaveChangesInterceptor>();
            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(configuration.GetConnectionString("NorthwindConnection"), builder => 
                builder.MigrationsAssembly(typeof(ApplicationDbContext).Assembly.FullName)));
            services.AddScoped<ICustomersRepository, CustomersRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<ICategoriesRepository, CategoriesRepository>();
            services.AddScoped<IDiscountRepository, DiscountRepository>();

            return services;
        }
    }
}