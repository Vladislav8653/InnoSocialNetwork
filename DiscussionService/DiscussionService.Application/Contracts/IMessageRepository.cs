using DiscussionService.Domain.Models;
using MongoDB.Bson;

namespace DiscussionService.Application.Contracts;

public interface IMessageRepository
{
    Task<Message> GetByTweetIdAsync(Guid tweetId);
    Task<IEnumerable<Message>> GetAllAsync();
    Task CreateAsync(Message message);
    Task DeleteAsync(ObjectId messageId);
    Task UpdateAsync(Message message, ObjectId messageId);
    
}