using CleanArchSample.Application.Interfaces.Repositories;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Domain.Entities;
using CleanArchSample.Persistence.Context;
using CleanArchSample.Persistence.Interceptors;
using CleanArchSample.Persistence.Repositories;
using CleanArchSample.Persistence.UnitOfWorks;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchSample.Persistence
{
    public static class ServiceRegistration
    {
        public static void AddPersistence(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<UpdateAuditableInterceptor>();
            services.AddDbContext<AppDbContext>((sp, opt) =>
            {
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
                opt.AddInterceptors(sp.GetRequiredService<UpdateAuditableInterceptor>());
            });

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
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
    }
}
