using CleanArchSample.Application.Interfaces.Repositories;
using CleanArchSample.Application.Interfaces.Rules;
using CleanArchSample.Domain.Entities;
using CleanArchSample.Persistence.Context;
using CleanArchSample.Persistence.Interceptors;
using CleanArchSample.Persistence.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CleanArchSample.Application.Data;
using CleanArchSample.Domain.Repositories;

namespace CleanArchSample.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            var assembly = Assembly.GetExecutingAssembly();

            services.AddSingleton<UpdateAuditableInterceptor>();
            services.AddDbContext<AppDbContext>((sp, opt) =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                opt.AddInterceptors(sp.GetRequiredService<UpdateAuditableInterceptor>());
            });

            services.AddReadRepositoriesFromAssemblyContaining(assembly);
            services.AddScoped(typeof(IGenericReadRepository<>), typeof(GenericReadRepository<>));
            services.AddScoped(typeof(IGenericWriteRepository<>), typeof(GenericWriteRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            services.AddIdentityCore<User>(opt =>
                {
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequiredLength = 2;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequireUppercase = false;
                    opt.Password.RequireDigit = false;
                    opt.SignIn.RequireConfirmedEmail = false;
                }).AddRoles<Role>()
                .AddEntityFrameworkStores<AppDbContext>();
        }
        private static void AddReadRepositoriesFromAssemblyContaining(this IServiceCollection services, Assembly assembly)
        {
            var interfaceType = typeof(IReadRepository);
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
