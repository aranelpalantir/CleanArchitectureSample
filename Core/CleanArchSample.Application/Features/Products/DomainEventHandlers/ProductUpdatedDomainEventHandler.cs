using System.Diagnostics;
using CleanArchSample.Domain.DomainEvents.Product;
using MediatR;

namespace CleanArchSample.Application.Features.Products.DomainEventHandlers
{
    internal class ProductUpdatedDomainEventHandler : INotificationHandler<ProductUpdatedDomainEvent>
    {
        public Task Handle(ProductUpdatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Product Updated:{notification.ProductId}");
            return Task.CompletedTask;
        }
    }
}
