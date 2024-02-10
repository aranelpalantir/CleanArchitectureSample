using FluentValidation;

namespace CleanArchSample.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandValidator : AbstractValidator<RefreshTokenCommandRequest>
    {
        public RefreshTokenCommandValidator()
        {
            RuleFor(p => p.AccessToken)
                .NotEmpty();

            RuleFor(p => p.RefreshTokenToken)
                .NotEmpty();
        }
    }
}
