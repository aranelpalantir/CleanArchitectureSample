using CleanArchSample.Application.Abstractions.MessageBroker;
using MassTransit;

namespace CleanArchSample.Infrastructure.MessageBroker
{
    public sealed class IntegrationEventBus(IPublishEndpoint publishEndpoint) : IIntegrationEventBus
    {
        public Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class =>
            publishEndpoint.Publish(message, cancellationToken);
    }
}
