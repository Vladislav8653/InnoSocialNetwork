using DiscussionService.Domain.Models;

namespace DiscussionService.Application;

public interface IMessageRepository
{
    Task<Message> GetByTweetIdAsync(Guid tweetId);
    Task<IEnumerable<Message>> GetAllAsync();
    Task CreateAsync(Message message);
    Task DeleteAsync(Guid messageId);
    Task UpdateAsync(Message message, Guid messageId);
    
}