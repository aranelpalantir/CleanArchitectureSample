using CleanArchSample.Domain.Entities;
using CleanArchSample.Persistence.Context;
using CleanArchSample.Persistence.Interceptors;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using CleanArchSample.Application.Data;
using CleanArchSample.Domain.Repositories;
using CleanArchSample.Persistence.Repositories;

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
            var genericInterfaceType = typeof(IRepository<>);
            var concreteTypes = assembly.GetTypes()
                .Where(t => t.GetInterfaces()
                    .Any(i => i.IsGenericType && i.GetGenericTypeDefinition() == genericInterfaceType) && t != typeof(BaseRepository<>))
                .ToList();

            foreach (var concreteType in concreteTypes)
            {
                var interfaceType = concreteType.GetInterfaces()
                    .Single(i => !i.IsGenericType);

                services.AddScoped(interfaceType, concreteType);
            }
        }
    }
}
