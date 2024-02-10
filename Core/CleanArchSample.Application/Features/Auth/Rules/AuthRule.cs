using CleanArchSample.Application.Features.Auth.Exceptions;
using CleanArchSample.Application.Interfaces.Rules;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchSample.Application.Features.Auth.Rules
{
    public class AuthRule : IBaseRule
    {
        private readonly UserManager<User> _userManager;

        public AuthRule(UserManager<User> userManger)
        {
            _userManager = userManger;
        }
        public async Task UserShouldNotBeExist(string email)
        {
            if (await _userManager.FindByEmailAsync(email) is not null)
                throw new UserAlreadyExistException();
        }

        public async Task EMailOrPasswordShouldNotBeInvalid(User? user, string password)
        {
            if (user == null)
                throw new EmailOrPasswordShouldNotBeInvalidException();

            var checkPassword = await _userManager.CheckPasswordAsync(user, password);
            if (!checkPassword)
                throw new EmailOrPasswordShouldNotBeInvalidException();
        }

        public Task RefreshTokenShouldNotBeExpired(DateTimeOffset? refreshTokenExpiry)
        {
            if (refreshTokenExpiry <= DateTimeOffset.Now)
                throw new RefreshTokenShouldNotBeExpiredException();
            return Task.CompletedTask;
        }
        public Task EmailAddressShouldBeValid(User? user)
        {
            if (user is null) throw new EmailAddressShouldBeValidException();
            return Task.CompletedTask;
        }
    }
}
