using CleanArchSample.Application.Interfaces.Rules;
using CleanArchSample.Domain.Entities;

namespace CleanArchSample.Application.Features.Auth.Rules;

internal interface IAuthRule : IBaseRule
{
    Task UserShouldNotBeExist(string email);
    Task EMailOrPasswordShouldNotBeInvalid(User? user, string password);
    Task RefreshTokenShouldNotBeExpired(DateTimeOffset? refreshTokenExpiry);
    Task EmailAddressShouldBeValid(User? user);
    Task UserShouldBeExist(User? user);
}