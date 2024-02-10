using AutoMapper;
using CleanArchSample.Application.Features.Common;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Application.Features.Products.Commands.UpdateProduct
{
    internal class UpdateProductCommandHandler : CqrsHandlerBase, IRequestHandler<UpdateProductCommandRequest, Unit>
    {
        public UpdateProductCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IHttpContextAccessor httpContextAccessor) : base(unitOfWork, mapper, httpContextAccessor)
        {
        }

        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await UnitOfWork.GetReadRepository<Product>().SingleAsync(r => r.Id == request.Id,
                rr => rr.Include(pc => pc.ProductCategories), cancellationToken: cancellationToken);

            Mapper.Map(request, product);

            product.LastModifiedBy = "-";
            product.LastModifiedDate = DateTimeOffset.UtcNow;

            product.ProductCategories.Clear();
            foreach (var categoryId in request.CategoryIds)
            {
                product.ProductCategories.Add(new ProductCategory { Product = product, CategoryId = categoryId });
            }

            await UnitOfWork.GetWriteRepository<Product>().UpdateAsync(product, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
