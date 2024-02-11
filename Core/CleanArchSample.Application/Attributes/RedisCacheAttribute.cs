namespace CleanArchSample.Application.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RedisCacheAttribute : Attribute
    {
        private const int DefaultCacheSeconds = 15;
        public int CacheSeconds { get; }

        public RedisCacheAttribute()
        {
            CacheSeconds = DefaultCacheSeconds;
        }
        public RedisCacheAttribute(int cacheSeconds)
        {
            CacheSeconds = cacheSeconds;
        }
    }
}
