using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CleanArchSample.Application.Middlewares
{
    public class RequestTimingMiddleware(ILogger<RequestTimingMiddleware> logger) : IMiddleware
    {
        private const int WarningThresholdMilliseconds = 3000;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();

            await next(context);

            stopwatch.Stop();

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            if (elapsedMilliseconds > WarningThresholdMilliseconds)
            {
                logger.LogWarning("Uzun süren istek: {ElapsedMilliseconds} ms - {RequestUrl}", elapsedMilliseconds, GetRequestUrl(context.Request));
            }
        }

        private static string GetRequestUrl(HttpRequest httpRequest)
        {
            return
                $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}{httpRequest.Path}{httpRequest.QueryString}";
        }
    }
}
