namespace CleanArchSample.Application.Attributes
{

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class RedisCacheAttribute : Attribute
    {
        private const int DefaultCacheMinutes = 1;
        public int CacheMinutes { get; }

        public RedisCacheAttribute()
        {
            CacheMinutes = DefaultCacheMinutes;
        }
        public RedisCacheAttribute(int cacheMinutes)
        {
            CacheMinutes = cacheMinutes;
        }
    }
}
