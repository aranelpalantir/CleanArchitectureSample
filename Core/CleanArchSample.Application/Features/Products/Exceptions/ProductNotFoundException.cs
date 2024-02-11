using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Products.Exceptions
{
    internal class ProductNotFoundException() : BaseRuleException("Ürün bulunamadı!")
    {
    }
}
