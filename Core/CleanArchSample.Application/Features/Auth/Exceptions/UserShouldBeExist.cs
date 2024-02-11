using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    public class UserShouldBeExist : BaseRuleException
    {
        public UserShouldBeExist() : base("Kullanıcı bulunamadı!")
        {

        }
    }
}
