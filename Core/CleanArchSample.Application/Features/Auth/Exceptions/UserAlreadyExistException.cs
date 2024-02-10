using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    public class UserAlreadyExistException : BaseRuleException
    {
        public UserAlreadyExistException() : base("Böyle bir kullanıcı zaten var!")
        {

        }
    }
}
