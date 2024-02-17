using CleanArchSample.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace CleanArchSample.Application.IntegrationTests
{
    public abstract class BaseIntegrationTest : IClassFixture<IntegrationTestWebAppFactory>
    {
        protected readonly ISender Sender;
        protected readonly AppDbContext DbContext;
        protected BaseIntegrationTest(IntegrationTestWebAppFactory factory)
        {
            var scope = factory.Services.CreateScope();
            Sender = scope.ServiceProvider.GetRequiredService<ISender>();
            DbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        }
    }
}
