namespace CleanArchSample.Application.Abstractions.Cache
{
    public interface ICacheService
    {
        Task<T?> GetAsync<T>(string key);
        Task SetAsync<T>(string key, T value, double? expirationMinute = null);
    }
}
