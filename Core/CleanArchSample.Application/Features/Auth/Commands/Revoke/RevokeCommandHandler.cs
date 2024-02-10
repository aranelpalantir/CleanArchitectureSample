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
    public class RevokeCommandHandler : CqrsHandlerBase, IRequestHandler<RevokeCommandRequest, Unit>
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthRule _authRule;

        public RevokeCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,
            AuthRule authRule) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _userManager = userManager;
            _authRule = authRule;
        }

        public async Task<Unit> Handle(RevokeCommandRequest request, CancellationToken cancellationToken)
        {
            User? user = await _userManager.FindByEmailAsync(request.Email);
            await _authRule.EmailAddressShouldBeValid(user);
            user.RefreshToken = null;
            user.RefreshTokenExpiry = null;
            await _userManager.UpdateAsync(user);
            return Unit.Value;
        }
    }
}
