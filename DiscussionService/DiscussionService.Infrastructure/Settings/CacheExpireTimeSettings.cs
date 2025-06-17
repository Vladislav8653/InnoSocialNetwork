namespace DiscussionService.Infrastructure.Settings;

public record CacheExpireTimeSettings
{
    public long MemoryCacheExpireTimeMinutes { get; init; }
    public long RedisCacheExpireTimeMinutes { get; init; }
}