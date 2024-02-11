using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal class UserAlreadyExistException() : BaseRuleException("Böyle bir kullanıcı zaten var!");
}
