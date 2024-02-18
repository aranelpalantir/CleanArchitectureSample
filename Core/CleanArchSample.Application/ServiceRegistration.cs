using System.Globalization;
using System.Reflection;
using CleanArchSample.Application.Abstractions.BusinessRule;
using CleanArchSample.Application.Behaviours;
using FluentValidation;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchSample.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddMediatR(cfg =>
            {
                cfg.RegisterServicesFromAssembly(assembly);
                cfg.AddOpenBehavior(typeof(FluentValidationBehaviour<,>));
                cfg.AddOpenBehavior(typeof(RequestCacheBehaviour<,>));
            });

            services.AddAutoMapper(assembly);

            services.AddValidatorsFromAssembly(assembly);
            ValidatorOptions.Global.LanguageManager.Culture = new CultureInfo("tr");
            services.AddRulesFromAssemblyContaining(assembly);
        }

        private static void AddRulesFromAssemblyContaining(this IServiceCollection services, Assembly assembly)
        {
            var interfaceType = typeof(IBaseBusinessRule);
            var types = assembly.GetTypes()
                .Where(t => t.GetInterfaces().Any(i => i == interfaceType) && t.IsClass)
                .ToList();

            foreach (var type in types)
            {
                services.AddScoped(type.GetInterfaces().Single(i => i.GetInterfaces().Contains(interfaceType)),
                    type);
            }
        }
    }
}
