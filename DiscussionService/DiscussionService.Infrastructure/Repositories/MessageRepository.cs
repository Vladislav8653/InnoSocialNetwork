using DiscussionService.Application.Contracts;
using DiscussionService.Application.Pagination;
using DiscussionService.Domain.Models;
using DiscussionService.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DiscussionService.Infrastructure.Repositories;

public class MessageRepository : IMessageRepository
{
    private readonly IMongoCollection<Message> _collection;

    public MessageRepository(IOptions<MongoDbSettings> settings, IMongoClient client)
    {
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<Message>(settings.Value.MessageDocument);
    }
    
    public async Task<Message> GetByIdAsync(ObjectId id) =>
        await _collection.Find(message => message.Id == id).SingleOrDefaultAsync();

    public async Task<IEnumerable<Message>> GetAllAsync(Guid tweetId) => 
        await _collection.Find(m => m.TweetId == tweetId).ToListAsync();

    public async Task CreateAsync(Message message) =>
        await _collection.InsertOneAsync(message);

    public async Task DeleteAsync(ObjectId messageId) =>
        await _collection.DeleteOneAsync(m => m.Id == messageId);

    public async Task UpdateAsync(Message message, ObjectId messageId) =>
        await _collection.ReplaceOneAsync(m => m.Id == messageId, message);
    
    public async Task<PagedResult<Message>> GetPagedAsync(Guid tweetId, PageParams pageParams)
    {
        var elements = await _collection
            .Find(m => m.TweetId == tweetId)
            .Skip((pageParams.Page - 1) * pageParams.PageSize)
            .Limit(pageParams.PageSize)
            .ToListAsync();
        var count = await _collection.CountDocumentsAsync(_ => true);
        return new PagedResult<Message>(elements, count);
    }

}