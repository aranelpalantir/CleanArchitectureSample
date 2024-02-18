﻿using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Products.Exceptions
{
    internal sealed class ProductNotFoundException() : BaseBusinessRuleException("Ürün bulunamadı!")
    {
    }
}
