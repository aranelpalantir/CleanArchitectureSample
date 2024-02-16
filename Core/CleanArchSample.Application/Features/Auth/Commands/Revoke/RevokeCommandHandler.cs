using CleanArchSample.Application.Features.Auth.Rules;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace CleanArchSample.Application.Features.Auth.Commands.Revoke
{
    internal sealed class RevokeCommandHandler(
        UserManager<User> userManager)
        :  IRequestHandler<RevokeCommandRequest, Unit>
    {
        public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await userManager.FindByEmailAsync(request.Email!);
            await AuthRule.EmailAddressShouldBeValid(user);
            user!.RefreshToken = null;
            user.RefreshTokenExpiry = null;
            await userManager.UpdateAsync(user);
            return Unit.Value;
        }
    }
}
