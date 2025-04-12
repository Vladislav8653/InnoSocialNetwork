using DiscussionService.Application.Pagination;
using DiscussionService.Domain.Models;
using MongoDB.Bson;

namespace DiscussionService.Application.Contracts;

public interface IMessageRepository
{
    Task<Message> GetByIdAsync(ObjectId id);
    Task<IEnumerable<Message>> GetAllAsync(Guid tweetId);
    Task CreateAsync(Message message);
    Task DeleteAsync(ObjectId messageId);
    Task UpdateAsync(Message message, ObjectId messageId);
    Task<PagedResult<Message>> GetPagedAsync(Guid tweetId, PageParams pageParams);
}