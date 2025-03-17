namespace MarCorp.DemoBack.Infrastructure.EventBus.Options
{
    public class RabbitMQOptions
    {
        public string HostName { get; init; }
        public string VirtualHost { get; init; }
        public string UserName { get; init; }
        public string Password { get; init; }
    }
}