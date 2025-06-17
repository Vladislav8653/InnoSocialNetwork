using DiscussionService.Application.Pagination;
using DiscussionService.Domain.Models;
using MongoDB.Bson;

namespace DiscussionService.Application.Contracts;

public interface IMessageRepository
{
    Task<Message?> GetByIdAsync(ObjectId id, CancellationToken cancellationToken);
    Task CreateAsync(Message message, CancellationToken cancellationToken);
    Task DeleteAsync(ObjectId messageId, CancellationToken cancellationToken);
    Task UpdateAsync(Message message, ObjectId messageId, CancellationToken cancellationToken);
    Task<PagedResult<Message>> GetPagedAsync(Guid tweetId, PageParams pageParams, CancellationToken cancellationToken);
}