using CleanArchSample.Domain.Primitives;

namespace CleanArchSample.Domain.DomainEvents.Product
{
    public sealed record ProductDeletedDomainEvent(int ProductId) : IDomainEvent
    {
    }
}
