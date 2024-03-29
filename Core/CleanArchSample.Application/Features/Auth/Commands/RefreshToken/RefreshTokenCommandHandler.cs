﻿using CleanArchSample.Application.Abstractions.Security;
using CleanArchSample.Application.Features.Auth.Rules;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchSample.Application.Features.Auth.Commands.RefreshToken
{
    internal sealed class RefreshTokenCommandHandler(
        UserManager<User> userManager,
        ITokenService tokenService,
        IAuthRule authRule) :
            IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var user = userManager.Users.SingleOrDefault(r => r.RefreshToken == request.RefreshToken);
            await authRule.UserShouldBeExist(user);
            await authRule.RefreshTokenShouldNotBeExpired(user!.RefreshTokenExpiry);

            var roles = await userManager.GetRolesAsync(user);
            var tokenModel = await tokenService.CreateTokenModel(user, roles);

            user.RefreshToken = tokenModel.RefreshToken;

            await userManager.UpdateAsync(user);
            await userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", tokenModel.Token);
            return new RefreshTokenCommandResponse
            {
                AccessToken = tokenModel.Token,
                RefreshToken = tokenModel.RefreshToken
            };
        }
    }
}
