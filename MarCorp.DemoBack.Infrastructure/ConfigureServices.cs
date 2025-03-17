using MarCorp.DemoBack.Application.Interface.Infrastructure;
using MarCorp.DemoBack.Infrastructure.EventBus.Options;
using MarCorp.DemoBack.Infrastructure.EventBus;
using Microsoft.Extensions.DependencyInjection;
using MassTransit;
using Microsoft.Extensions.Options;

namespace MarCorp.DemoBack.Infrastructure
{
    public static class ConfigureServices
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services)
        {
            services.ConfigureOptions<RabbitMQOptionsSetup>();
            services.AddScoped<IEventBus, EventBusRabbitMQ>();
            services.AddMassTransit(x =>
            {
                x.UsingRabbitMq((context, cfg) =>
                {
                    RabbitMQOptions? opt = services.BuildServiceProvider()
                        .GetRequiredService<IOptions<RabbitMQOptions>>()
                        .Value;

                    cfg.Host(opt.HostName, opt.VirtualHost, h =>
                    {
                        h.Username(opt.UserName);
                        h.Password(opt.Password);
                    });

                    cfg.ConfigureEndpoints(context);
                });
            });

            return services;
        }
    }
}