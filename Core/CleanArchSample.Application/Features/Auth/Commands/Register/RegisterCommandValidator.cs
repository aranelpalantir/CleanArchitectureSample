using FluentValidation;

namespace CleanArchSample.Application.Features.Auth.Commands.Register
{
    internal class RegisterCommandValidator : AbstractValidator<RegisterCommandRequest>
    {
        public RegisterCommandValidator()
        {
            RuleFor(p => p.FullName)
                .NotEmpty()
                .MaximumLength(50)
                .MinimumLength(2)
                .WithName("Ad Soyad");

            RuleFor(p => p.Email)
                .NotEmpty()
                .MaximumLength(60)
                .MinimumLength(8)
                .EmailAddress()
                .WithName("E-Posta Adresi");

            RuleFor(p => p.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithName("Parola");

            RuleFor(p => p.ConfirmPassword)
                .NotEmpty()
                .MinimumLength(6)
                .Equal(p => p.Password)
                .WithName("Parola Tekrarı")
                .WithMessage("Parolalar uyuşmuyor!");
        }
    }
}
