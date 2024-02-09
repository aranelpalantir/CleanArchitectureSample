﻿using CleanArchSample.Application.Features.Products.Exceptions;
using CleanArchSample.Application.Interfaces.Repositories;
using CleanArchSample.Application.Interfaces.Rules;
using CleanArchSample.Domain.Entities;

namespace CleanArchSample.Application.Features.Products.Rules
{
    public class ProductRule : IBaseRule
    {
        public async Task ProductTitleMustNotBeSame(IReadRepository<Product> repository, string title, CancellationToken cancellationToken)
        {
            if (await repository.AnyAsync(r => r.Title == title, cancellationToken))
                throw new ProductTitleMustNotBeSameException();
        }
    }
}
