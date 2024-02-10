using AutoMapper;
using CleanArchSample.Application.Features.Common;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchSample.Application.Features.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : CqrsHandlerBase, IRequestHandler<DeleteProductCommandRequest, Unit>
    {
        public DeleteProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(DeleteProductCommandRequest request, CancellationToken cancellationToken)
        {
            await UnitOfWork.GetWriteRepository<Product>().RemoveAsync(request.Id, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
