using CleanArchSample.Application.Abstractions.Security;
using CleanArchSample.Application.IntegrationTests.Stubs;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Microsoft.Extensions.DependencyInjection;
using Testcontainers.MsSql;

namespace CleanArchSample.Application.IntegrationTests.Abstractions;

public class IntegrationTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private static readonly int MssqlPort = new Random().Next(1440, 1500);

    private static readonly string MssqlPassword = "d3YYOOrr0bQluNiQLtF2";

    private static readonly MsSqlContainer DbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
        .WithPortBinding(MssqlPort, 1433)
        .WithPassword(MssqlPassword)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("IntegrationTest");
        builder.UseSetting("ConnectionStrings:DefaultConnection",
            $"Server=localhost,{MssqlPort};Database=master;User Id=sa;Password={MssqlPassword};TrustServerCertificate=true;");
        builder.ConfigureTestServices(services =>
        {
            var descriptor = services
                .SingleOrDefault(s => s.ServiceType == typeof(IUserContext));
            if (descriptor != null)
                services.Remove(descriptor);
            services.AddScoped<IUserContext, TestUserContext>();
        });
    }
    public async Task InitializeAsync()
    {
        await DbContainer.StartAsync();
    }
    public new async Task DisposeAsync()
    {
        await DbContainer.StopAsync();
    }
}