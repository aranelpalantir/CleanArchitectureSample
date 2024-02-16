using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CleanArchSample.Application.Middlewares
{
    internal sealed class RequestTimingMiddleware(RequestDelegate next)
    {
        private const int WarningThresholdMilliseconds = 3000;

        public async Task InvokeAsync(HttpContext httpContext, ILogger<RequestTimingMiddleware> logger)
        {
            var stopwatch = Stopwatch.StartNew();

            await next(httpContext);

            stopwatch.Stop();

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            if (elapsedMilliseconds > WarningThresholdMilliseconds)
            {
                logger.LogWarning("Uzun süren istek: {ElapsedMilliseconds} ms - {RequestUrl}", elapsedMilliseconds, GetRequestUrl(httpContext.Request));
            }
        }

        private static string GetRequestUrl(HttpRequest httpRequest)
        {
            return
                $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}{httpRequest.Path}{httpRequest.QueryString}";
        }
    }
}
