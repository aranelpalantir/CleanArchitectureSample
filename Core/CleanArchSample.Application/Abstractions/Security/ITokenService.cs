using System.Security.Claims;
using CleanArchSample.Application.Models;
using CleanArchSample.Domain.Entities;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchSample.Application.Abstractions.Security
{
    public interface ITokenService
    {
        Task<TokenModel> CreateTokenModel(User user, IList<string> roles);
        ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token);
        TokenValidationParameters GetTokenValidationParameters();
    }
}
