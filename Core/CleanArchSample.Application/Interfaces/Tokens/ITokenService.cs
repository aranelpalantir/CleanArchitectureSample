using CleanArchSample.Domain.Entities;
using System.Security.Claims;
using CleanArchSample.Application.Models;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchSample.Application.Interfaces.Tokens
{
    public interface ITokenService
    {
        Task<TokenModel> CreateTokenModel(User user, IList<string> roles);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
        TokenValidationParameters GetTokenValidationParameters();
    }
}
