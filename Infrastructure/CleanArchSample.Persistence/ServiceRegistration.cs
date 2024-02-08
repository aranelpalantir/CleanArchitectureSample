using CleanArchSample.Application.Interfaces.Repositories;
using CleanArchSample.Application.Interfaces.UnitOfWorks;
using CleanArchSample.Persistence.Context;
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
            services.AddDbContext<AppDbContext>(opt =>
                opt.UseSqlServer(configuration.GetConnectionString("DefaultConnection")));

            services.AddScoped(typeof(IReadRepository<>), typeof(ReadRepository<>));
            services.AddScoped(typeof(IWriteRepository<>), typeof(WriteRepository<>));
            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));
        }
    }
}
