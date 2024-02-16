using AutoMapper;
using CleanArchSample.Application.Features.Products.Exceptions;
using CleanArchSample.Application.Features.Products.Rules;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Application.Features.Products.Commands.UpdateProduct
{
    internal sealed class UpdateProductCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper):
        IRequestHandler<UpdateProductCommandRequest, Unit>
    {
        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await unitOfWork.GetReadRepository<Product>().SingleOrDefaultAsync(r => r.Id == request.Id,
                              rr => rr.Include(pc => pc.ProductCategories), cancellationToken: cancellationToken) ??
                          throw new ProductNotFoundException();

            if (product.Title != request.Title)
                await ProductRule.ProductTitleMustNotBeSame(unitOfWork.GetReadRepository<Product>(), request.Title!,
                    cancellationToken);

            mapper.Map(request, product);

            product.ProductCategories.Clear();
            foreach (var categoryId in request.CategoryIds!)
            {
                product.ProductCategories.Add(new ProductCategory { Product = product, CategoryId = categoryId });
            }

            await unitOfWork.GetWriteRepository<Product>().UpdateAsync(product, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
