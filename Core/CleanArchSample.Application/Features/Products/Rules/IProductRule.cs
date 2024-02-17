using CleanArchSample.Application.Interfaces.Rules;

namespace CleanArchSample.Application.Features.Products.Rules
{
    internal interface IProductRule : IBaseRule
    {
        Task ProductTitleMustNotBeSame(string title, CancellationToken cancellationToken);
    }
}
