using MarCorp.DemoBack.Application.Interface.UseCases;
using MarCorp.DemoBack.Application.UseCases.Categories;
using MarCorp.DemoBack.Application.UseCases.Customers;
using MarCorp.DemoBack.Application.UseCases.Users;
using Microsoft.Extensions.DependencyInjection;

namespace MarCorp.DemoBack.Application.UseCases
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<ICategoriesApplication, CategoriesApplication>();

            return services;
        }
    }
}
