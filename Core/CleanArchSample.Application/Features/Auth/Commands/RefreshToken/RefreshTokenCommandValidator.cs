using FluentValidation;

namespace CleanArchSample.Application.Features.Auth.Commands.RefreshToken
{
    internal class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommandRequest>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(p => p.RefreshToken)
                .NotEmpty();
        }
    }
}
