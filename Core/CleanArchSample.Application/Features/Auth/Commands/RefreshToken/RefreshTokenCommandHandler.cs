using AutoMapper;
using CleanArchSample.Application.Features.Auth.Rules;
using CleanArchSample.Application.Features.Common;
using CleanArchSample.Application.Interfaces.Tokens;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArchSample.Application.Features.Auth.Commands.RefreshToken
{
    public class RefreshTokenCommandHandler : CqrsHandlerBase, IRequestHandler<RefreshTokenCommandRequest, RefreshTokenCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly ITokenService _tokenService;
        private readonly AuthRule _authRule;

        public RefreshTokenCommandHandler(IUnitOfWork unitOfWork,
            IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,
            ITokenService tokenService,
            AuthRule authRule) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _userManager = userManager;
            _tokenService = tokenService;
            _authRule = authRule;
        }

        public async Task<RefreshTokenCommandResponse> Handle(RefreshTokenCommandRequest request, CancellationToken cancellationToken)
        {
            var user = _userManager.Users.SingleOrDefault(r => r.RefreshToken == request.RefreshToken);
            var roles = await _userManager.GetRolesAsync(user);
            await _authRule.RefreshTokenShouldNotBeExpired(user.RefreshTokenExpiry);
            var tokenModel = await _tokenService.CreateTokenModel(user, roles);
            user.RefreshToken = tokenModel.RefreshToken;
            await _userManager.UpdateAsync(user);
            await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", tokenModel.Token);
            return new RefreshTokenCommandResponse
            {
                AccessToken = tokenModel.Token,
                RefreshToken = tokenModel.RefreshToken
            };
        }
    }
}
