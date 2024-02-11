using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal sealed class RefreshTokenShouldNotBeExpiredException() : BaseRuleException("Oturum süreniz dolmuştur!");
}
