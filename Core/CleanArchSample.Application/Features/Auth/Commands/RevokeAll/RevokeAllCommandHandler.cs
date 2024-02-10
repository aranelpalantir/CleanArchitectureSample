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
    public class RevokeAllCommandHandler : CqrsHandlerBase, IRequestHandler<RevokeAllCommandRequest, Unit>
    {
        private readonly UserManager<User> _userManager;

        public RevokeAllCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _userManager = userManager;
        }

        public async Task<Unit> Handle(RevokeAllCommandRequest request, CancellationToken cancellationToken)
        {
            var users = await _userManager.Users.ToListAsync(cancellationToken);
            foreach (var user in users)
            {
                user.RefreshToken = null;
                user.RefreshTokenExpiry = null;
                await _userManager.UpdateAsync(user);
            }
            return Unit.Value;
        }
    }
}
