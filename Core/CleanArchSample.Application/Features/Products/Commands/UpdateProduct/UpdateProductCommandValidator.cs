using FluentValidation;

namespace CleanArchSample.Application.Features.Products.Commands.UpdateProduct
{
    public sealed class UpdateProductCommandValidator : AbstractValidator<UpdateProductCommandRequest>
    {
        public UpdateProductCommandValidator()
        {
            RuleFor(p => p.Id)
                .GreaterThan(0);
            RuleFor(p => p.Title)
                .NotEmpty()
                .WithName("Başlık");
            RuleFor(p => p.Description)
                .NotEmpty()
                .WithName("Açıklama");
            RuleFor(p => p.BrandId)
                .GreaterThan(0)
                .WithName("Marka");
            RuleFor(p => p.Price)
                .GreaterThan(0)
                .WithName("Fiyat");
            RuleFor(p => p.Discount)
                .GreaterThanOrEqualTo(0)
                .WithName("İmdirim Oranı");
            RuleFor(p => p.CategoryIds)
                .NotEmpty()
                .Must(p => p.Any())
                .WithName("Kategoriler");
        }
    }
}
