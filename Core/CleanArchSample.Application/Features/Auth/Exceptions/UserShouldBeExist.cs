using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal class UserShouldBeExist() : BaseRuleException("Kullanıcı bulunamadı!");
}
