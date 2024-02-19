using CleanArchSample.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace CleanArchSample.Api.Infrastructure
{
    public static class Migration
    {
        public static void MigrateDb(this WebApplication app)
        {
            using (var scope = app.Services.CreateScope())
            {
                var services = scope.ServiceProvider;

                try
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    dbContext.Database.Migrate();
                }
                catch (Exception ex)
                {
                    var logger = services.GetRequiredService<ILogger<Program>>();
                    logger.LogError(ex, "An error occurred while migrating the database.");
                }
            }
        }
    }
}
