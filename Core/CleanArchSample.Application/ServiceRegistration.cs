using System.Globalization;
using System.Reflection;
using CleanArchSample.Application.Behaviours;
using CleanArchSample.Application.Interfaces.Rules;
using CleanArchSample.Application.Middlewares;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchSample.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

            services.AddAutoMapper(assembly);

            services.AddTransient<RequestTimingMiddleware>();
            services.AddTransient<ExceptionMiddleware>();

            services.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(FluentValidationBehaviour<,>));
            services.AddTransient(typeof(IPipelineBehavior<,>), typeof(RedisCacheBehaviour<,>));

            services.AddRulesFromAssembylContaining(assembly, typeof(IBaseRule));
        }

        private static void AddRulesFromAssembylContaining(this IServiceCollection services,
            Assembly assembly, Type type)
        {
            var types = assembly.GetTypes().Where(t => t.GetInterfaces().Contains(type)).ToList();
            foreach (var item in types)
            {
                services.AddTransient(item);
            }
        }
    }
}
