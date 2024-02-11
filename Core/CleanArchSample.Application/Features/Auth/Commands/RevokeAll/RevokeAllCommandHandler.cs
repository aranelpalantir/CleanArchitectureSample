using AutoMapper;
using CleanArchSample.Application.Features.Common;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Application.Features.Auth.Commands.RevokeAll
{
    internal class RevokeAllCommandHandler(
        IUnitOfWork unitOfWork,
        IMapper mapper,
        IHttpContextAccessor httpContextAccessor,
        UserManager<User> userManager)
        : CqrsHandlerBase(unitOfWork, mapper, httpContextAccessor), IRequestHandler<RevokeAllCommandRequest, Unit>
    {
        public async Task<Unit> Handle(RevokeAllCommandRequest request, CancellationToken cancellationToken)
        {
            var users = await userManager.Users.ToListAsync(cancellationToken);
            foreach (var user in users)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiry = null;
                await userManager.UpdateAsync(user);
            }
            return Unit.Value;
        }
    }
}
