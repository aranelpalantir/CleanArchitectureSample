using CleanArchSample.Application.Features.Products.Exceptions;
using CleanArchSample.Application.Interfaces.Repositories;
using CleanArchSample.Domain.Entities;

namespace CleanArchSample.Application.Features.Products.Rules
{
    internal class ProductRule(IGenericReadRepository<Product>repository) : IProductRule
    {
        public async Task ProductTitleMustNotBeSame(string title, CancellationToken cancellationToken)
        {
            if (await repository.AnyAsync(r => r.Title == title, cancellationToken))
                throw new ProductTitleMustNotBeSameException();
        }
    }
}
