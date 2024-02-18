using System.Text.Json;
using CleanArchSample.Application.Abstractions.Cache;
using CleanArchSample.Application.Attributes;
using MediatR;

namespace CleanArchSample.Application.Behaviours
{
    internal sealed class RequestCacheBehaviour<TRequest, TResponse>(ICacheService cacheService)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var type = typeof(TRequest);
            var hasRequestCacheAttribute = Attribute.IsDefined(type, typeof(RequestCacheAttribute));
            if (hasRequestCacheAttribute)
            {
                var key = GenerateKey(request);
                var cachedData = await cacheService.GetAsync<TResponse>(key);
                if (cachedData != null)
                    return cachedData;

                var response = await next();

                var requestCacheAttribute = (RequestCacheAttribute?)Attribute.GetCustomAttribute(type, typeof(RequestCacheAttribute));
                if (response != null && requestCacheAttribute != null)
                    await cacheService.SetAsync<TResponse>(key, response, requestCacheAttribute.CacheSeconds);
            }

            return await next();
        }
        private static string GenerateKey(TRequest queryObject)
        {
            var typeName = typeof(TRequest).FullName;
            var parameterString = JsonSerializer.Serialize(queryObject);
            var cacheKey = $"{typeName}:{parameterString}";
            return cacheKey;
        }

    }
}
