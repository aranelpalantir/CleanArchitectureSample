using CleanArchSample.Application.Data;
using CleanArchSample.Domain.DomainEvents.Product;
using CleanArchSample.Domain.Repositories;
using MediatR;

namespace CleanArchSample.Application.Features.Products.Commands.DeleteProduct
{
    internal sealed class DeleteProductCommandHandler(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IPublisher publisher) :
        IRequestHandler<DeleteProductCommandRequest, Unit>
    {
        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            await productRepository.RemoveAsync(request.Id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            await publisher.Publish(new ProductDeletedDomainEvent(request.Id), cancellationToken);
            return Unit.Value;
        }
    }
}
