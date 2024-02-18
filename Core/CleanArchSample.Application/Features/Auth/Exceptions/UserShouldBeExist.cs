using CleanArchSample.Application.Exceptions;

namespace CleanArchSample.Application.Features.Auth.Exceptions
{
    internal sealed class UserShouldBeExist() : BaseBusinessRuleException("Kullanıcı bulunamadı!");
}
