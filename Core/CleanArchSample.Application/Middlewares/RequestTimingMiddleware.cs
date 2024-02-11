using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System.Diagnostics;

namespace CleanArchSample.Application.Middlewares
{
    public class RequestTimingMiddleware : IMiddleware
    {
        private readonly ILogger<RequestTimingMiddleware> _logger;
        private const int WarningThresholdMilliseconds = 3000;

        public RequestTimingMiddleware(ILogger<RequestTimingMiddleware> logger)
        {
            _logger = logger;
        }
        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var stopwatch = Stopwatch.StartNew();

            await next(context);

            stopwatch.Stop();

            var elapsedMilliseconds = stopwatch.ElapsedMilliseconds;

            if (elapsedMilliseconds > WarningThresholdMilliseconds)
            {
                _logger.LogWarning($"Uzun süren istek: {elapsedMilliseconds} ms - {GetRequestUrl(context.Request)}");
            }
        }

        private static string GetRequestUrl(HttpRequest httpRequest)
        {
            return
                $"{httpRequest.Scheme}://{httpRequest.Host}{httpRequest.PathBase}{httpRequest.Path}{httpRequest.QueryString}";
        }
    }
}
