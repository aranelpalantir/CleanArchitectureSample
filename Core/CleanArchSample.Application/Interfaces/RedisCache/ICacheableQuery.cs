namespace CleanArchSample.Application.Interfaces.RedisCache
{
    public interface ICacheableQuery
    {
        int? CacheMinute { get; }
    }
}
