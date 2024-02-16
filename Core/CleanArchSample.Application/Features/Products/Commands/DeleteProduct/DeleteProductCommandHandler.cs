using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;

namespace CleanArchSample.Application.Features.Products.Commands.DeleteProduct
{
    internal sealed class DeleteProductCommandHandler(
        IUnitOfWork unitOfWork) :
        IRequestHandler<DeleteProductCommandRequest, Unit>
    {
        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            await unitOfWork.GetWriteRepository<Product>().RemoveAsync(request.Id, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
