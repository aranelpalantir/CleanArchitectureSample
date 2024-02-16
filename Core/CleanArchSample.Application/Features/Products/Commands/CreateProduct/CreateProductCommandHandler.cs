using AutoMapper;
using CleanArchSample.Application.Features.Products.Rules;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.DomainEvents.Product;
using CleanArchSample.Domain.Entities;
using MediatR;

namespace CleanArchSample.Application.Features.Products.Commands.CreateProduct
{
    internal sealed class CreateProductCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IPublisher publisher) : 
        IRequestHandler<CreateProductCommandRequest, Unit>
    {
        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await ValidateRules(request, cancellationToken);

            var product = mapper.Map<Product>(request);

            await unitOfWork.GetWriteRepository<Product>().AddAsync(product, cancellationToken);

            foreach (var categoryId in request.CategoryIds)
            {
                await unitOfWork.GetWriteRepository<ProductCategory>().AddAsync(new ProductCategory
                {
                    Product = product,
                    CategoryId = categoryId
                }, cancellationToken);
            }
            await unitOfWork.SaveChangesAsync(cancellationToken);

            await publisher.Publish(new ProductCreatedDomainEvent(product.Id), cancellationToken);
         
            return Unit.Value;
        }

        private async Task ValidateRules(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await ProductRule.ProductTitleMustNotBeSame(unitOfWork.GetReadRepository<Product>(), request.Title!, cancellationToken);
        }
    }
}
