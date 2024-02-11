using AutoMapper;
using CleanArchSample.Application.Features.Common;
using CleanArchSample.Application.Features.Products.Commands.CreateProduct;
using CleanArchSample.Application.Features.Products.Exceptions;
using CleanArchSample.Application.Features.Products.Rules;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Application.Features.Products.Commands.UpdateProduct
{
    internal sealed class UpdateProductCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
        : CqrsHandlerBase(unitOfWork, mapper, httpContextAccessor), IRequestHandler<UpdateProductCommandRequest, Unit>
    {
        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await UnitOfWork.GetReadRepository<Product>().SingleOrDefaultAsync(r => r.Id == request.Id,
                              rr => rr.Include(pc => pc.ProductCategories), cancellationToken: cancellationToken) ??
                          throw new ProductNotFoundException();

            if (product.Title != request.Title)
                await ProductRule.ProductTitleMustNotBeSame(UnitOfWork.GetReadRepository<Product>(), request.Title!,
                    cancellationToken);

            Mapper.Map(request, product);

            product.ProductCategories.Clear();
            foreach (var categoryId in request.CategoryIds!)
            {
                product.ProductCategories.Add(new ProductCategory { Product = product, CategoryId = categoryId });
            }

            await UnitOfWork.GetWriteRepository<Product>().UpdateAsync(product, cancellationToken);
            await UnitOfWork.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
