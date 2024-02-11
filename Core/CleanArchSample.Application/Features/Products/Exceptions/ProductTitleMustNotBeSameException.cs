using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Products.Exceptions
{
    internal sealed class ProductTitleMustNotBeSameException() : BaseRuleException("Ürün başlığı zaten var!");
}
