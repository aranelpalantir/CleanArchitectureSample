using FluentValidation;

namespace CleanArchSample.Application.Features.Products.Commands.DeleteProduct
{
    public sealed class DeleteProductCommandValidator : AbstractValidator<DeleteProductCommandRequest>
    {
        public DeleteProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0);
        }
    }
}
