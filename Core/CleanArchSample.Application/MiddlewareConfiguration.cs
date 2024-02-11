using CleanArchSample.Application.Middlewares;
using Microsoft.AspNetCore.Builder;

namespace CleanArchSample.Application
{
    public static class MiddlewareConfiguration
    {
        public static void ConfigureApplicationMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<RequestTimingMiddleware>();
            app.UseMiddleware<ExceptionMiddleware>();
        }
    }
}
