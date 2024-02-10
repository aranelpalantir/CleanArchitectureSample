using CleanArchSample.Application.Interfaces.Tokens;
using CleanArchSample.Infrastructure.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using System;

namespace CleanArchSample.Infrastructure
{
    public static class ServicesRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection("JWT"));
            services.AddTransient<ITokenService, TokenService>();
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                var serviceProvider = services.BuildServiceProvider();
                var serviceScopeFactory = serviceProvider.GetRequiredService<IServiceScopeFactory>();
                using var scope = serviceScopeFactory.CreateScope();
                var scopeServiceProvider = scope.ServiceProvider;
                var tokenService = scopeServiceProvider.GetRequiredService<ITokenService>();
                opt.SaveToken = true;
                opt.TokenValidationParameters = tokenService.GetTokenValidationParameters();
            });
        }
    }
}
