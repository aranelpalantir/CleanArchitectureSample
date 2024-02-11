using AutoMapper;
using CleanArchSample.Application.Features.Auth.Rules;
using CleanArchSample.Application.Features.Common;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArchSample.Application.Features.Auth.Commands.Revoke
{
    internal class RevokeCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        UserManager<User> userManager)
        : CqrsHandlerBase(unitOfWork, mapper, httpContextAccessor), IRequestHandler<RevokeCommandRequest, Unit>
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
