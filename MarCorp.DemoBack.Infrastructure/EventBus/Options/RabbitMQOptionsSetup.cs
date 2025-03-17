using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

namespace MarCorp.DemoBack.Infrastructure.EventBus.Options
{
    public class RabbitMQOptionsSetup : IConfigureOptions<RabbitMQOptions>
    {
        private const string ConfigurationSectionName = "RabbitMQOptions";
        private readonly IConfiguration _configuration;

        public RabbitMQOptionsSetup(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public void Configure(RabbitMQOptions options)
        {
            _configuration.GetSection(ConfigurationSectionName).Bind(options);
        }
    }
}