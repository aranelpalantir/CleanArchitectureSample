using AutoMapper;
using CleanArchSample.Application.Data;
using CleanArchSample.Application.Features.Products.Exceptions;
using CleanArchSample.Application.Features.Products.Rules;
using CleanArchSample.Domain.DomainEvents.Product;
using CleanArchSample.Domain.Entities;
using CleanArchSample.Domain.Repositories;
using MediatR;

namespace CleanArchSample.Application.Features.Products.Commands.UpdateProduct
{
    internal sealed class UpdateProductCommandHandler(
        IUnitOfWork unitOfWork,
        IProductRepository productRepository,
        IMapper mapper,
        IPublisher publisher,
        IProductRule productRule) :
        IRequestHandler<UpdateProductCommandRequest, Unit>
    {
        public async Task<Unit> Handle(UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            var product = await GetProductFromDataBase(request.Id, cancellationToken);

            await ValidateProductRules(product, request, cancellationToken);

            product = MapToProductEntity(request, product);

            await unitOfWork.SaveChangesAsync(cancellationToken);

            await publisher.Publish(new ProductUpdatedDomainEvent(product.Id), cancellationToken);

            return Unit.Value;
        }

        private async Task<Product> GetProductFromDataBase(int id, CancellationToken cancellationToken)
        {
            var product = await productRepository.GetByIdWithProductCategories(id, cancellationToken) ??
                          throw new ProductNotFoundException();

            return product;
        }
        private async Task ValidateProductRules(Product product, UpdateProductCommandRequest request, CancellationToken cancellationToken)
        {
            if (product.Title != request.Title)
                await productRule.ProductTitleMustNotBeSame(request.Title!, cancellationToken);
        }
        private Product MapToProductEntity(UpdateProductCommandRequest request, Product product)
        {
            mapper.Map(request, product);
            product.ProductCategories.Clear();
            foreach (var categoryId in request.CategoryIds!)
            {
                product.ProductCategories.Add(new ProductCategory { Product = product, CategoryId = categoryId });
            }
            return product;
        }
    }
}
