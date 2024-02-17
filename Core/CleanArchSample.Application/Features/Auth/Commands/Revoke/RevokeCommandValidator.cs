using FluentValidation;

namespace CleanArchSample.Application.Features.Auth.Commands.Revoke
{
    public sealed class RevokeCommandValidator : AbstractValidator<RevokeCommandRequest>
    {
        public RevokeCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress()
                .NotEmpty();
        }
    }
}
