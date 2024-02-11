using System.Text.Json;
using CleanArchSample.Application.Interfaces.RedisCache;
using StackExchange.Redis;

namespace CleanArchSample.Infrastructure.RedisCache
{
    internal class RedisCacheService(IDatabase database, IConnectionMultiplexer connectionMultiplexer)
        : IRedisCacheService
    {
        public async Task<T?> GetAsync<T>(string key)
        {
            if (!connectionMultiplexer.IsConnected)
                return default;

            var value = await database.StringGetAsync(key);
            if (value.HasValue)
                return JsonSerializer.Deserialize<T>(value.ToString());

            return default;
        }

        public async Task SetAsync<T>(string key, T value, double? expirationSeconds = null)
        {
            if (!connectionMultiplexer.IsConnected)
                return;

            TimeSpan? expiry = null;
            if (expirationSeconds != null)
                expiry = TimeSpan.FromSeconds(expirationSeconds.Value);

            await database.StringSetAsync(key, JsonSerializer.Serialize<T>(value), expiry);
        }
    }
}
