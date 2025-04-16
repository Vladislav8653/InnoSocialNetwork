using DiscussionService.Application.Contracts;
using DiscussionService.Application.Pagination;
using DiscussionService.Domain.Models;
using DiscussionService.Infrastructure.Settings;
using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Options;
using MongoDB.Bson;

namespace DiscussionService.Infrastructure.Repositories;

public class CachedMessageRepository(
    IOptions<CacheExpireTimeSettings> options,
    MessageRepository messageRepository,
    IMemoryCache memoryCache) : IMessageRepository
{
    public async Task<Message?> GetByIdAsync(ObjectId id, CancellationToken cancellationToken)
    {
        return await memoryCache.GetOrCreateAsync(
            id,
            entry =>
            {
                entry.SetAbsoluteExpiration(TimeSpan.FromMinutes(options.Value.ImMemoryCacheExpireTimeMinutes));

                return messageRepository.GetByIdAsync(id, cancellationToken);
            });
    }

    public async Task<IEnumerable<Message>> GetAllAsync(Guid tweetId, CancellationToken cancellationToken) =>
        await messageRepository.GetAllAsync(tweetId, cancellationToken);

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