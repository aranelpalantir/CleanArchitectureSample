namespace CleanArchSample.Api.Infrastructure
{
    public static class ApplicationConfiguration
    {
        public static void ConfigureSwagger(this WebApplication app)
        {
            if (app.Environment.IsDevelopment() || app.Environment.EnvironmentName == "DockerCompose")
            {
                app.UseSwagger();
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Web Api v1");
                    c.SwaggerEndpoint("/swagger/v2/swagger.json", "Web Api v2");
                });
            }
        }
    }
}
