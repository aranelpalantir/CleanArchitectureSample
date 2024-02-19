using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using StackExchange.Redis;
using CleanArchSample.Infrastructure.Security;
using MassTransit;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using RabbitMQ.Client;
using System.Reflection;
using CleanArchSample.Application.Abstractions.Cache;
using CleanArchSample.Application.Abstractions.MessageBroker;
using CleanArchSample.Application.Abstractions.Security;
using CleanArchSample.Application.Features.Brands.IntegrationEvents;
using CleanArchSample.Infrastructure.Cache;
using CleanArchSample.Infrastructure.MessageBroker;

namespace CleanArchSample.Infrastructure
{
    public static class ServiceRegistration
    {
        public static void AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TokenSettings>(configuration.GetSection("JWT"));
            services.AddTransient<ITokenService, TokenService>();
            services.Configure<RedisCacheSettings>(configuration.GetSection("RedisCache"));
            services.AddTransient<ICacheService, RedisCacheService>();

            services.Configure<RabbitMqSettings>(configuration.GetSection("RabbitMQ"));

            var serviceScopeFactory = services.BuildServiceProvider().GetRequiredService<IServiceScopeFactory>();
            RedisCacheSettings redisCacheSettings;
            RabbitMqSettings rabbitMqSettings;
            ITokenService tokenService;
            using (var scope = serviceScopeFactory.CreateScope())
            {
                redisCacheSettings = scope.ServiceProvider.GetRequiredService<IOptions<RedisCacheSettings>>().Value;
                rabbitMqSettings = scope.ServiceProvider.GetRequiredService<IOptions<RabbitMqSettings>>().Value;
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

            services.AddSingleton<IConnectionFactory>(new ConnectionFactory
            {
                HostName = rabbitMqSettings.Host,
                UserName = rabbitMqSettings.Username,
                Password = rabbitMqSettings.Password
            });


            services.AddMassTransit(busConfigurator =>
            {
                busConfigurator.AddConsumers(Assembly.GetAssembly(typeof(BrandCreatedIntegrationEventConsumer)));
                busConfigurator.SetKebabCaseEndpointNameFormatter();
                busConfigurator.UsingRabbitMq((context, configurator) =>
                {
                    configurator.Host(new Uri($"amqp://{rabbitMqSettings.Host}"), h =>
                    {
                        h.Username(rabbitMqSettings.Username);
                        h.Password(rabbitMqSettings.Password);
                    });
                    configurator.ConfigureEndpoints(context);
                });
            });

            services.AddTransient<IIntegrationEventBus, IntegrationEventBus>();

            services.AddHealthChecks()
                .AddSqlServer(configuration.GetConnectionString("DefaultConnection")!)
                .AddRedis(redisCacheSettings.Configuration)
                .AddRabbitMQ(rabbitMqSettings.Host, HealthStatus.Unhealthy);

            services
                .AddHealthChecksUI(setupSettings: setup =>
                {
                    setup.SetEvaluationTimeInSeconds(5); // Configures the UI to poll for healthchecks updates every 5 seconds
                    setup.SetApiMaxActiveRequests(1);//Only one active request will be executed at a time. All the excedent requests will result in 429 (Too many requests)
                    setup.AddHealthCheckEndpoint("endpoint1", "http://localhost:8080/health");
                    //setup.AddHealthCheckEndpoint("endpoint2", "http://localhost:8001/health");
                    //setup.AddHealthCheckEndpoint("endpoint3", "http://remoteendpoint:9000/health");
                    //setup.AddWebhookNotification("webhook1", uri: "https://healthchecks.requestcatcher.com/",
                    //    payload: "{ message: \"Webhook report for [[LIVENESS]]: [[FAILURE]] - Description: [[DESCRIPTIONS]]\"}",
                    //    restorePayload: "{ message: \"[[LIVENESS]] is back to life\"}");
                    setup.SetNotifyUnHealthyOneTimeUntilChange(); // You will only receive one failure notification until the status changes.
                }).AddInMemoryStorage();
        }
    }
}
