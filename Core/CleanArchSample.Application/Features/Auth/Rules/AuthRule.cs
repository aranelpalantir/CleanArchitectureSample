using CleanArchSample.Application.Features.Auth.Exceptions;
using CleanArchSample.Application.Interfaces.Rules;
using CleanArchSample.Domain.Entities;
using Microsoft.AspNetCore.Identity;

namespace CleanArchSample.Application.Features.Auth.Rules
{
    internal sealed class AuthRule(UserManager<User> userManger) : IBaseRule
    {
        public async Task UserShouldNotBeExist(string email)
        {
            if (await userManger.FindByEmailAsync(email) is not null)
                throw new UserAlreadyExistException();
        }

        public async Task EMailOrPasswordShouldNotBeInvalid(User? user, string password)
        {
            if (user == null)
                throw new EmailOrPasswordShouldNotBeInvalidException();

            var checkPassword = await userManger.CheckPasswordAsync(user, password);
            if (!checkPassword)
                throw new EmailOrPasswordShouldNotBeInvalidException();
        }

        public static Task RefreshTokenShouldNotBeExpired(DateTimeOffset? refreshTokenExpiry)
        {
            if (refreshTokenExpiry <= DateTimeOffset.Now)
                throw new RefreshTokenShouldNotBeExpiredException();
            return Task.CompletedTask;
        }
        public static Task EmailAddressShouldBeValid(User? user)
        {
            if (user is null) throw new EmailAddressShouldBeValidException();
            return Task.CompletedTask;
        }
        public static Task UserShouldBeExist(User? user)
        {
            if (user is null) throw new UserShouldBeExist();
            return Task.CompletedTask;
        }
    }
}
