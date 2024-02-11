using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal class RefreshTokenShouldNotBeExpiredException() : BaseRuleException("Oturum süreniz dolmuştur!");
}
