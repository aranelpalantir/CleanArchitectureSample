﻿using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Products.Exceptions
{
    internal class ProductTitleMustNotBeSameException() : BaseRuleException("Ürün başlığı zaten var!");
}
