using CleanArchSample.Domain.Primitives;

namespace CleanArchSample.Domain.DomainEvents.Product
{
    public sealed record ProductUpdatedDomainEvent(int ProductId) : IDomainEvent
    {
    }
}
