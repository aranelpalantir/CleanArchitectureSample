using AutoMapper;
using CleanArchSample.Application.Abstractions.Data;
using CleanArchSample.Application.Features.Products.Rules;
using CleanArchSample.Domain.DomainEvents.Product;
using CleanArchSample.Domain.Entities;
using CleanArchSample.Domain.Repositories;
using MediatR;

namespace CleanArchSample.Application.Features.Products.Commands.CreateProduct
{
    internal sealed class CreateProductCommandHandler(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IMapper mapper,
        IPublisher publisher,
        IProductRule productRule) :
        IRequestHandler<CreateProductCommandRequest, Unit>
    {
        public async Task<Unit> Handle(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await ValidateProductRules(request, cancellationToken);
            var product = MapToProductEntity(request);
            await SaveProductToDatabase(product, cancellationToken);
            await publisher.Publish(new ProductCreatedDomainEvent(product.Id), cancellationToken);
            return Unit.Value;
        }
        private async Task ValidateProductRules(CreateProductCommandRequest request, CancellationToken cancellationToken)
        {
            await productRule.ProductTitleMustNotBeSame(request.Title!, cancellationToken);
        }
        private Product MapToProductEntity(CreateProductCommandRequest request)
        {
            var product = mapper.Map<Product>(request);
            foreach (var categoryId in request.CategoryIds)
            {
                product.ProductCategories.Add(new ProductCategory
                {
                    Product = product,
                    CategoryId = categoryId
                });
            }
            return product;
        }
        private async Task SaveProductToDatabase(Product product, CancellationToken cancellationToken)
        {
            await productRepository.AddAsync(product, cancellationToken);
            await unitOfWork.SaveChangesAsync(cancellationToken);
        }
    }
}
