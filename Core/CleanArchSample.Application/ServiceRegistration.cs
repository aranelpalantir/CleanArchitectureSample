using System.Reflection;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchSample.Application
{
    public static class ServiceRegistration
    {
        public static void AddApplication(this IServiceCollection services)
        {
            var assembly = Assembly.GetExecutingAssembly();
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(assembly));

        }
    }
}
