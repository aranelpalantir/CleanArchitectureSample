namespace CleanArchSample.Application.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    internal sealed class RedisCacheAttribute(int cacheSeconds) : Attribute
    {
        private const int DefaultCacheSeconds = 15;
        public int CacheSeconds { get; } = cacheSeconds;

        public RedisCacheAttribute() : this(DefaultCacheSeconds)
        {
        }
    }
}
