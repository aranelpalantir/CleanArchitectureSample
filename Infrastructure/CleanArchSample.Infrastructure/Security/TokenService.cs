using CleanArchSample.Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using CleanArchSample.Application.Abstractions.Security;
using CleanArchSample.Application.Models;
using Microsoft.IdentityModel.Tokens;

namespace CleanArchSample.Infrastructure.Security
{
    internal sealed class TokenService(IOptions<TokenSettings> tokenSettingsOptions, UserManager<User> userManager)
        : ITokenService
    {
        private readonly TokenSettings _tokenSettings = tokenSettingsOptions.Value;

        public async Task<TokenModel> CreateTokenModel(User user, IList<string> roles)
        {
            var token = await CreateToken(user, roles);
            var refreshToken = GenerateRefreshToken();
            return new TokenModel
            {
                Token = token,
                RefreshToken = refreshToken,
                RefreshTokenExpiry = DateTimeOffset.UtcNow.AddDays(_tokenSettings.RefreshTokenValidityInDays)
            };
        }
        private async Task<string> CreateToken(User user, IEnumerable<string> roles)
        {
            var claims = new List<Claim>
            {
                new(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new(ClaimTypes.NameIdentifier,user.Id.ToString()),
                new(ClaimTypes.Name,user.UserName!),
                new(JwtRegisteredClaimNames.Email,user.Email!),
            };
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret));
            var token = new JwtSecurityToken(
                issuer: _tokenSettings.Issuer,
                audience: _tokenSettings.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.AddMinutes(_tokenSettings.TokenValidityInMinutes),
                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            var existingClaims = await userManager.GetClaimsAsync(user);
            var newClaims = claims.Where(r => !existingClaims.Select(rr => rr.Type).Contains(r.Type)).ToList();
            var oldClaims = existingClaims.Where(r => !claims.Select(rr => rr.Type).Contains(r.Type)).ToList();
            if (oldClaims.Count != 0)
                await userManager.RemoveClaimsAsync(user, oldClaims);
            if (newClaims.Count != 0)
                await userManager.AddClaimsAsync(user, newClaims);
            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private static string GenerateRefreshToken()
        {
            var randomNumber = new byte[64];
            using var rnd = RandomNumberGenerator.Create();
            rnd.GetBytes(randomNumber);
            return Convert.ToBase64String(randomNumber);
        }

        public ClaimsPrincipal? GetPrincipalFromExpiredToken(string? token)
        {
            var tokenValidationParameters = GetTokenValidationParameters();
            JwtSecurityTokenHandler tokenHandler = new();
            var principal =
                tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken);
            if (securityToken is not JwtSecurityToken jwtSecurityToken ||
                !jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha256,
                    StringComparison.InvariantCultureIgnoreCase))
                throw new SecurityTokenException("Token bulunamadı!");
            return principal;
        }

        public TokenValidationParameters GetTokenValidationParameters()
        {
            return new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_tokenSettings.Secret)),
                ValidateLifetime = true,
                ValidIssuer = _tokenSettings.Issuer,
                ValidAudience = _tokenSettings.Audience,
                ClockSkew = TimeSpan.Zero
            };
        }
    }
}
