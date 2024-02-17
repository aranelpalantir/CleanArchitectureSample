using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using CleanArchSample.Application.Interfaces.RedisCache;
using CleanArchSample.Infrastructure.RedisCache;
using StackExchange.Redis;
using CleanArchSample.Application.Interfaces.Security;
using CleanArchSample.Infrastructure.Security;

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
            RedisCacheSettings redisCacheSettings;
            ITokenService tokenService;
            using (var scope = serviceScopeFactory.CreateScope())
            {
                redisCacheSettings = scope.ServiceProvider.GetRequiredService<IOptions<RedisCacheSettings>>().Value;
                tokenService = scope.ServiceProvider.GetRequiredService<ITokenService>();
            }
            
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
            {
                opt.SaveToken = true;
                opt.TokenValidationParameters = tokenService.GetTokenValidationParameters();
            });
            services.AddStackExchangeRedisCache(opt =>
            {
                opt.Configuration = redisCacheSettings.Configuration;
                opt.InstanceName = redisCacheSettings.InstanceName;
            });
            services.AddSingleton<IConnectionMultiplexer>(cfg =>
            {
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
