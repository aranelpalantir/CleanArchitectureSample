using AutoMapper;
using CleanArchSample.Application.Features.Common;
using CleanArchSample.Application.Features.Products.Rules;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CleanArchSample.Application.Features.Products.Commands.CreateProduct
{
    internal sealed class CreateProductCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor)
        : CqrsHandlerBase(unitOfWork, mapper, httpContextAccessor), IRequestHandler<CreateProductCommandRequest, Unit>
    {
        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await ValidateRules(request, cancellationToken);

            var product = Mapper.Map<Product>(request);

            await UnitOfWork.GetWriteRepository<Product>().AddAsync(product, cancellationToken);

            foreach (var categoryId in request.CategoryIds)
            {
                await UnitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new ProductCategory
                {
                    Product = product,
                    CategoryId = categoryId
                }, cancellationToken);
            }
            await UnitOfWork.SaveChangesAsync(cancellationToken);

            return Unit.Value;
        }

        private async Task ValidateRules(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await ProductRule.ProductTitleMustNotBeSame(UnitOfWork.GetReadRepository<Product>(), request.Title!, cancellationToken);
        }
    }
}
