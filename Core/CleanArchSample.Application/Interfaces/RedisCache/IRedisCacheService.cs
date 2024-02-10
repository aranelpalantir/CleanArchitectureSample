namespace CleanArchSample.Application.Interfaces.RedisCache
{
    public interface IRedisCacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, double? expirationMinute = null);
    }
}
