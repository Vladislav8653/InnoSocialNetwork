using DiscussionService.Application;
using DiscussionService.Domain.Models;
using DiscussionService.Infrastructure.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Bson;
using MongoDB.Driver;

namespace DiscussionService.Infrastructure;

public class MessageRepository : IMessageRepository
{
    private readonly IMongoCollection<Message> _collection;

    public MessageRepository(IOptions<MongoDbSettings> settings)
    {
        var client = new MongoClient(settings.Value.ConnectionString);
        var database = client.GetDatabase(settings.Value.DatabaseName);
        _collection = database.GetCollection<Message>(settings.Value.MessageDocument);
    }
    
    public async Task<Message> GetByTweetIdAsync(Guid tweetId) =>
        await _collection.Find(m => m.TweetId == tweetId).FirstOrDefaultAsync();

    public async Task<IEnumerable<Message>> GetAllAsync() => 
        await _collection.Find(_ => true).ToListAsync();

    public async Task CreateAsync(Message message) =>
        await _collection.InsertOneAsync(message);

    public async Task DeleteAsync(ObjectId messageId) =>
        await _collection.DeleteOneAsync(m => m.Id == messageId);

    public async Task UpdateAsync(Message message, ObjectId messageId) =>
        await _collection.ReplaceOneAsync(m => m.Id == messageId, message);
}