using CleanArchSample.Api.Infrastructure;
using CleanArchSample.Application;
using CleanArchSample.Persistence;
using CleanArchSample.Infrastructure;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Serilog;

try
{
    var builder = WebApplication.CreateBuilder(args);

    var env = builder.Environment;
    builder.Configuration
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json", optional: false)
        .AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true);

    builder.Services.AddControllers();

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.ConfigureSwagger();
    builder.Services.AddHttpContextAccessor();
    builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

    builder.Services.AddPersistence(builder.Configuration);
    builder.Services.AddInfrastructure(builder.Configuration);
    builder.Services.AddApplication();

    builder.Services.ConfigureApiVersioning();

    Log.Logger = new LoggerConfiguration()
        .WriteTo.Console()
        .CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog();

    builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
    builder.Services.AddProblemDetails();

    Log.Information("Starting web application");

    var app = builder.Build();

    app.ConfigureSwagger();

    app.UseHttpsRedirection();

    app.ConfigureApplicationMiddleware();
    app.UseExceptionHandler();

    app.UseRouting()
        .UseAuthorization()
        .UseEndpoints(config => config.MapHealthChecksUI(opt =>
        {
            opt.UIPath = "/health-ui";
        }));

    app.MapHealthChecks("health", new HealthCheckOptions
    {
        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
    });

    app.MapControllers();

    app.MigrateDb();

    app.Run();
}
catch (Exception ex)
{
    Log.Fatal(ex, "Application terminated unexpectedly");
}
finally
{
    Log.CloseAndFlush();
}

public partial class Program { }