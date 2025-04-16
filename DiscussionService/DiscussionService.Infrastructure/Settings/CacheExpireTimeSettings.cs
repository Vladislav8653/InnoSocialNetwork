namespace DiscussionService.Infrastructure.Settings;

public record CacheExpireTimeSettings
{
    public long ImMemoryCacheExpireTimeMinutes { get; init; }
    public long RedisCacheExpireTimeMinutes { get; init; }
}