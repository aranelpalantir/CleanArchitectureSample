using System.Diagnostics;
using CleanArchSample.Domain.DomainEvents.Product;
using MediatR;

namespace CleanArchSample.Application.Features.Products.DomainEventHandlers
{
    internal sealed class ProductCreatedDomainEventHandler : INotificationHandler<ProductCreatedDomainEvent>
    {
        public Task Handle(ProductCreatedDomainEvent notification, CancellationToken cancellationToken)
        {
            Debug.WriteLine($"Product Created:{notification.ProductId}");
            return Task.CompletedTask;
        }
    }
}
