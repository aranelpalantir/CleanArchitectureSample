using CleanArchSample.Application.Features.Auth.Rules;
using CleanArchSample.Application.Interfaces.Security;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchSample.Application.Features.Auth.Commands.Login
{
    internal sealed class LoginCommandHandler(
        UserManager<User> userManager,
        IAuthRule authRule,
        ITokenService tokenService) :
        IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email!);
            await authRule.EMailOrPasswordShouldNotBeInvalid(user, request.Password!);
            var roles = await userManager.GetRolesAsync(user!);
            var tokenModel = await tokenService.CreateTokenModel(user!, roles);
            user!.RefreshToken = tokenModel.RefreshToken;
            user.RefreshTokenExpiry = tokenModel.RefreshTokenExpiry;
            await userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", tokenModel.Token);
            return new LoginCommandResponse
            {
                Token = tokenModel.Token,
                RefreshToken = tokenModel.RefreshToken,
                RefreshTokenExpiry = tokenModel.RefreshTokenExpiry
            };
        }
    }
}
