using CleanArchSample.Application.Abstractions.MessageBroker;

namespace CleanArchSample.Application.Features.Brands.IntegrationEvents
{
    public sealed record BrandCreatedIntegrationEvent : IIntegrationEvent
    {
        public int Id { get; init; }
        public string Name { get; init; } = string.Empty;
    }
}
