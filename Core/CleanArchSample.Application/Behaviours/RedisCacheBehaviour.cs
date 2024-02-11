using System.Text.Json;
using CleanArchSample.Application.Attributes;
using CleanArchSample.Application.Interfaces.RedisCache;
using MediatR;

namespace CleanArchSample.Application.Behaviours
{
    public class RedisCacheBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        private readonly IRedisCacheService _redisCacheService;

        public RedisCacheBehaviour(IRedisCacheService redisCacheService)
        {
            _redisCacheService = redisCacheService;
        }
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var type = typeof(TRequest);
            var hasRedisCacheAttribute = Attribute.IsDefined(type, typeof(RedisCacheAttribute));
            if (hasRedisCacheAttribute)
            {
                var redisError = false;
                var key = GenerateKey(request);
                try
                {
                    var cachedData = await _redisCacheService.GetAsync<TResponse>(key);
                    if (cachedData != null)
                        return cachedData;
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    redisError = true;
                }

                var response = await next();
                try
                {
                    var redisCacheAttribute = (RedisCacheAttribute)Attribute.GetCustomAttribute(type, typeof(RedisCacheAttribute));
                    if (!redisError && response != null)
                        await _redisCacheService.SetAsync<TResponse>(key, response, redisCacheAttribute.CacheSeconds);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                }
            }

            return await next();
        }
        public static string GenerateKey(TRequest queryObject)
        {
            var typeName = typeof(TRequest).FullName;
            var parameterString = JsonSerializer.Serialize(queryObject);
            var cacheKey = $"{typeName}:{parameterString}";
            return cacheKey;
        }

    }
}
