using System.Diagnostics;
using CleanArchSample.Domain.DomainEvents.Product;
using MediatR;

namespace CleanArchSample.Application.Features.Products.Events
{
    internal sealed class ProductDeletedDomainEventHandler : INotificationHandler<ProductDeletedDomainEvent>
    {
        public Task Handle(ProductDeletedDomainEvent notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Product Deleted:{notification.ProductId}");
            return Task.CompletedTask;
        }
    }
}
