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
    public static class InjectionExtensions
    {
        public static IServiceCollection AddInjection(this IServiceCollection services, IConfiguration configuration)
        {

            services.AddSingleton<IConfiguration>(configuration);
            services.AddSingleton<IConnectionFactory, ConnectionFactory>();
            services.AddScoped<ICustomerApplication, CustomerApplication>();
            services.AddScoped<ICustomerDomain, CustomerDomain>();
            services.AddScoped<ICustomerRepository, CustomerRepository>();
            services.AddScoped<IUsersRepository, UsersRepository>();
            services.AddScoped<IUsersApplication, UsersApplication>();
            services.AddScoped<IUsersDomain, UsersDomain>();
            services.AddScoped(typeof(IAppLogger<>), typeof(LoggerAdapter<>));

            return services;
        }
    }
}
