using System.Text.Json;
using DiscussionService.Application.Contracts;
using DiscussionService.Application.Pagination;
using DiscussionService.Domain.Models;
using DiscussionService.Infrastructure.Settings;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace DiscussionService.Infrastructure.Repositories;

public class CachedMessageRepository(
    IOptions<CacheExpireTimeSettings> options,
    MessageRepository messageRepository,
    IMemoryCache memoryCache,
    IDistributedCache distributedCache) : IMessageRepository
{
    public async Task<Message?> GetByIdAsync(ObjectId id, CancellationToken cancellationToken)
    {
        if (memoryCache.TryGetValue(id, out Message? cachedMessage))
            return cachedMessage;
        
        var redisBytes = await distributedCache.GetAsync(id.ToString(), cancellationToken);
        if (redisBytes is not null)
        {
            var redisMessage = JsonSerializer.Deserialize<Message>(redisBytes);
            if (redisMessage is not null)
            {
                memoryCache.Set(id, redisMessage, TimeSpan.FromMinutes(options.Value.MemoryCacheExpireTimeMinutes));
                return redisMessage;
            }
        }
        
        var dbMessage = await messageRepository.GetByIdAsync(id, cancellationToken);
        
        if (dbMessage is null) 
            return dbMessage;
        
        var bytes = JsonSerializer.SerializeToUtf8Bytes(dbMessage);
        await distributedCache.SetAsync(
            key: id.ToString(),
            value: bytes,
            options: new DistributedCacheEntryOptions
            {
                AbsoluteExpirationRelativeToNow = TimeSpan.FromMinutes(options.Value.RedisCacheExpireTimeMinutes)
            },
            cancellationToken
        );
            
        memoryCache.Set(id, dbMessage, TimeSpan.FromMinutes(options.Value.MemoryCacheExpireTimeMinutes));

        return dbMessage;
    }

    public async Task CreateAsync(Message message, CancellationToken cancellationToken) => 
        await messageRepository.CreateAsync(message, cancellationToken);

    public async Task DeleteAsync(ObjectId messageId, CancellationToken cancellationToken) =>
        await messageRepository.DeleteAsync(messageId, cancellationToken);

    public async Task UpdateAsync(Message message, ObjectId messageId, CancellationToken cancellationToken) =>
        await messageRepository.UpdateAsync(message, messageId, cancellationToken);

    public async Task<PagedResult<Message>> GetPagedAsync(Guid tweetId, PageParams pageParams,
        CancellationToken cancellationToken) =>
        await messageRepository.GetPagedAsync(tweetId, pageParams, cancellationToken);
}