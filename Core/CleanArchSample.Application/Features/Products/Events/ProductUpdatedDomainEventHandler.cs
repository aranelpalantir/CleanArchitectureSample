using CleanArchSample.Domain.DomainEvents.Product;
using MediatR;
using System.Diagnostics;

namespace CleanArchSample.Application.Features.Products.Events
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
