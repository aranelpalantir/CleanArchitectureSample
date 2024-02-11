namespace CleanArchSample.Infrastructure.RedisCache
{
    internal class RedisCacheSettings
    {
        public required string Configuration { get; set; }
        public required string InstanceName { get; set; }
    }
}
