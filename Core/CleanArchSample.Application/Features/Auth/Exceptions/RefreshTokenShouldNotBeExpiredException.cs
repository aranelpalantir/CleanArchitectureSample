using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal sealed class RefreshTokenShouldNotBeExpiredException() : BaseBusinessRuleException("Oturum süreniz dolmuştur!");
}
