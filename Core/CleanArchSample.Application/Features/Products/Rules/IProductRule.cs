using CleanArchSample.Application.Abstractions.BusinessRule;

namespace CleanArchSample.Application.Features.Products.Rules
{
    internal interface IProductRule : IBaseBusinessRule
    {
        Task ProductTitleMustNotBeSame(string title, CancellationToken cancellationToken);
    }
}
