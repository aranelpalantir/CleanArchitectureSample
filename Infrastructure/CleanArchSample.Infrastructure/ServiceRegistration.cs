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

            using var scope = serviceScopeFactory.CreateScope();
            var redisCacheSettings = scope.ServiceProvider.GetRequiredService<IOptions<RedisCacheSettings>>().Value;

            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection")!)
                .AddRedis(redisCacheSettings.Configuration);

            services
                .AddHealthChecksUI(setupSettings: setup =>
                {
                    setup.SetEvaluationTimeInSeconds(5); // Configures the UI to poll for healthchecks updates every 5 seconds
                    setup.SetApiMaxActiveRequests(1);//Only one active request will be executed at a time. All the excedent requests will result in 429 (Too many requests)
                    setup.AddHealthCheckEndpoint("endpoint1", "/health");
                    setup.AddHealthCheckEndpoint("endpoint2", "http://localhost:8001/health");
                    setup.AddHealthCheckEndpoint("endpoint3", "http://remoteendpoint:9000/health");
                    setup.AddWebhookNotification("webhook1", uri: "https://healthchecks.requestcatcher.com/",
                        payload: "{ message: \"Webhook report for [[LIVENESS]]: [[FAILURE]] - Description: [[DESCRIPTIONS]]\"}",
                        restorePayload: "{ message: \"[[LIVENESS]] is back to life\"}");
                    setup.SetNotifyUnHealthyOneTimeUntilChange(); // You will only receive one failure notification until the status changes.
        }).AddInMemoryStorage();
        }
    }
}
