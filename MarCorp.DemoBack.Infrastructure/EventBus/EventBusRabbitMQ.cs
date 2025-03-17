using MarCorp.DemoBack.Application.Interface.Infrastructure;
using MassTransit;

namespace MarCorp.DemoBack.Infrastructure.EventBus
{
    class EventBusRabbitMQ : IEventBus
    {
        private readonly IPublishEndpoint _publishEndpoint;

        public EventBusRabbitMQ(IPublishEndpoint publishEndpoint)
        {
            _publishEndpoint = publishEndpoint;
        }

        public async void Publish<T>(T @event)
        {
            await _publishEndpoint.Publish(@event);
        }
    }
}