using CleanArchSample.Application.Interfaces.Tokens;
using CleanArchSample.Infrastructure.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using CleanArchSample.Application.Interfaces.RedisCache;
using CleanArchSample.Infrastructure.RedisCache;
using StackExchange.Redis;

namespace CleanArchSample.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection("JWT"));
            services.AddTransient<ITokenService, TokenService>();
            services.Configure<RedisCacheSettings>(configuration.GetSection("RedisCache"));
            services.AddTransient<IRedisCacheService, RedisCacheService>();

            var serviceScopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();

            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                using var scope = serviceScopeFactory.CreateScope();
                var tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
                opt.SaveToken = true;
                opt.TokenValidationParameters = tokenService.GetTokenValidationParameters();
            });
            services.AddStackExchangeRedisCache(opt =>
            {
                using var scope = serviceScopeFactory.CreateScope();
                var redisCacheSettings = scope.ServiceProvider.GetRequiredService<IOptions<RedisCacheSettings>>().Value;
                opt.Configuration = redisCacheSettings.Configuration;
                opt.InstanceName = redisCacheSettings.InstanceName;
            });
            services.AddSingleton<IConnectionMultiplexer>(cfg =>
            {
                var redisCacheSettings = cfg.GetRequiredService<IOptions<RedisCacheSettings>>().Value;
                var multiplexer = ConnectionMultiplexer.Connect(redisCacheSettings.Configuration, opt =>
                {
                    opt.ConnectTimeout = 1000;
                    opt.AsyncTimeout = 1000;
                    opt.SyncTimeout = 1000;
                });
                return multiplexer;
            });
            services.AddScoped<IDatabase>(cfg =>
            {
                var connectionMultiplexer = cfg.GetRequiredService<IConnectionMultiplexer>();
                return connectionMultiplexer.GetDatabase();
            });
        }
    }
}
