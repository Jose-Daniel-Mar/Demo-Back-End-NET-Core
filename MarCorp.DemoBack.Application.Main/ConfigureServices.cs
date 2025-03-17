using FluentValidation;
using MarCorp.DemoBack.Application.DTO;
using MarCorp.DemoBack.Application.Interface.UseCases;
using MarCorp.DemoBack.Application.UseCases.Categories;
using MarCorp.DemoBack.Application.UseCases.Customers;
using MarCorp.DemoBack.Application.UseCases.Users;
using MarCorp.DemoBack.Application.Validator;
using Microsoft.Extensions.DependencyInjection;
using Pacagroup.Ecommerce.Application.UseCases.Discounts;
using System.Reflection;

namespace MarCorp.DemoBack.Application.UseCases
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
            services.AddScoped<ICustomersApplication, CustomersApplication>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<ICategoriesApplication, CategoriesApplication>();
            services.AddScoped<IDiscountsApplication, DiscountsApplication>();
            services.AddTransient<IValidator<UserDTO>, UsersDTOValidator>();
            services.AddTransient<IValidator<DiscountDTO>, DiscountDTOValidator>();

            return services;
        }
    }
}
