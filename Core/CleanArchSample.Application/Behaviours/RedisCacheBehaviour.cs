using System.Text.Json;
using CleanArchSample.Application.Attributes;
using CleanArchSample.Application.Interfaces.RedisCache;
using MediatR;

namespace CleanArchSample.Application.Behaviours
{
    internal class RedisCacheBehaviour<TRequest, TResponse>(IRedisCacheService redisCacheService)
        : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
    {
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var type = typeof(TRequest);
            var hasRedisCacheAttribute = Attribute.IsDefined(type, typeof(RedisCacheAttribute));
            if (hasRedisCacheAttribute)
            {
                var key = GenerateKey(request);
                var cachedData = await redisCacheService.GetAsync<TResponse>(key);
                if (cachedData != null)
                    return cachedData;

                var response = await next();

                var redisCacheAttribute = (RedisCacheAttribute?)Attribute.GetCustomAttribute(type, typeof(RedisCacheAttribute));
                if (response != null && redisCacheAttribute != null)
                    await redisCacheService.SetAsync<TResponse>(key, response, redisCacheAttribute.CacheSeconds);
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
