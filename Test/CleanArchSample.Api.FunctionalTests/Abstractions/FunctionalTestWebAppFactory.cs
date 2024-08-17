using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.AspNetCore.TestHost;
using Testcontainers.MsSql;

namespace CleanArchSample.Api.FunctionalTests.Abstractions;

public class FunctionalTestWebAppFactory : WebApplicationFactory<Program>, IAsyncLifetime
{
    private static readonly int MssqlPort = new Random().Next(1440, 1500);

    private static readonly string MssqlPassword = "zOF8As7uvwKY7VclJga8";

    private static readonly MsSqlContainer DbContainer = new MsSqlBuilder()
        .WithImage("mcr.microsoft.com/mssql/server:2019-latest")
        .WithPortBinding(MssqlPort, 1433)
        .WithPassword(MssqlPassword)
        .Build();

    protected override void ConfigureWebHost(IWebHostBuilder builder)
    {
        builder.UseEnvironment("FunctionalTest");
        builder.UseSetting("ConnectionStrings:DefaultConnection",
            $"Server=localhost,{MssqlPort};Database=master;User Id=sa;Password={MssqlPassword};TrustServerCertificate=true;");
        builder.ConfigureTestServices(services =>
        {

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