using CleanArchSample.Domain.Primitives;

namespace CleanArchSample.Domain.DomainEvents.Product
{
    public sealed record ProductCreatedDomainEvent(int ProductId) : IDomainEvent
    {
    }
}
