using CleanArchSample.Application.Features.Products.Exceptions;
using CleanArchSample.Domain.Repositories;

namespace CleanArchSample.Application.Features.Products.Rules
{
    internal class ProductRule(IProductRepository productRepository) : IProductRule
    {
        public async Task ProductTitleMustNotBeSame(string title, CancellationToken cancellationToken)
        {
            if (await productRepository.IsProductTitleExistAsync(title, cancellationToken))
                throw new ProductTitleMustNotBeSameException();
        }
    }
}
