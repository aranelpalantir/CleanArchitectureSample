namespace CleanArchSample.Infrastructure.Cache
{
    internal sealed class RedisCacheSettings
    {
        public required string Configuration { get; set; }
        public required string InstanceName { get; set; }
    }
}
