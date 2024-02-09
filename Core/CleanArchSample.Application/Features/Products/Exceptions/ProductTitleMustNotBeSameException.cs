using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Products.Exceptions
{
    public class ProductTitleMustNotBeSameException : BaseRuleException
    {
        public ProductTitleMustNotBeSameException() : base("Ürün başlığı zaten var!") { }
    }
}
