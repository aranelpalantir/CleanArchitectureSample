namespace CleanArchSample.Application.Abstractions.MessageBroker;

public interface IIntegrationEventBus
{
    Task PublishAsync<T>(T message, CancellationToken cancellationToken = default) where T : class;
}