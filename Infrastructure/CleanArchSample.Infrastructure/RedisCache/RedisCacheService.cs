using System.Text.Json;
using CleanArchSample.Application.Interfaces.RedisCache;
using StackExchange.Redis;

namespace CleanArchSample.Infrastructure.RedisCache
{
    public class RedisCacheService : IRedisCacheService
    {
        private readonly IDatabase _database;
        private readonly IConnectionMultiplexer _connectionMultiplexer;

        public RedisCacheService(IDatabase database, IConnectionMultiplexer connectionMultiplexer)
        {
            _database = database;
            _connectionMultiplexer = connectionMultiplexer;
        }
        public async Task<T?> GetAsync<T>(string key)
        {
            if (!_connectionMultiplexer.IsConnected)
                return default;

            var value = await _database.StringGetAsync(key);
            if (value.HasValue)
                return JsonSerializer.Deserialize<T>(value);

            return default;
        }

        public async Task SetAsync<T>(string key, T value, double? expirationMinute = null)
        {
            if (!_connectionMultiplexer.IsConnected)
                return;

            TimeSpan? expiry = null;
            if (expirationMinute != null)
                expiry = TimeSpan.FromMinutes(expirationMinute.Value);

            await _database.StringSetAsync(key, JsonSerializer.Serialize<T>(value), expiry);
        }
    }
}
