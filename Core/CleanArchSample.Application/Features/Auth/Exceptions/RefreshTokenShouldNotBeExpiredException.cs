using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    public class RefreshTokenShouldNotBeExpiredException : BaseRuleException
    {
        public RefreshTokenShouldNotBeExpiredException() : base("Oturum süreniz dolmuştur!")
        {

        }
    }
}
