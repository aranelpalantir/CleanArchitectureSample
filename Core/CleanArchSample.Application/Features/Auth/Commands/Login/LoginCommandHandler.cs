using AutoMapper;
using CleanArchSample.Application.Features.Auth.Rules;
using CleanArchSample.Application.Features.Common;
using CleanArchSample.Application.Interfaces.Tokens;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;

namespace CleanArchSample.Application.Features.Auth.Commands.Login
{
    internal class LoginCommandHandler : CqrsHandlerBase, IRequestHandler<LoginCommandRequest, LoginCommandResponse>
    {
        private readonly UserManager<User> _userManager;
        private readonly AuthRule _authRule;
        private readonly ITokenService _tokenService;

        public LoginCommandHandler(IUnitOfWork unitOfWork, IMapper mapper,
            IHttpContextAccessor httpContextAccessor,
            UserManager<User> userManager,
            AuthRule authRule,
            ITokenService tokenService) : base(unitOfWork, mapper, httpContextAccessor)
        {
            _userManager = userManager;
            _authRule = authRule;
            _tokenService = tokenService;
        }

        public async Task<LoginCommandResponse> Handle(LoginCommandRequest request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            await _authRule.EMailOrPasswordShouldNotBeInvalid(user, request.Password);
            var roles = await _userManager.GetRolesAsync(user);
            var tokenModel = await _tokenService.CreateTokenModel(user, roles);
            user.RefreshToken = tokenModel.RefreshToken;
            user.RefreshTokenExpiry = tokenModel.RefreshTokenExpiry;
            await _userManager.SetAuthenticationTokenAsync(user, "Default", "AccessToken", tokenModel.Token);
            return new LoginCommandResponse
            {
                Token = tokenModel.Token,
                RefreshToken = tokenModel.RefreshToken,
                RefreshTokenExpiry = tokenModel.RefreshTokenExpiry
            };
        }
    }
}
