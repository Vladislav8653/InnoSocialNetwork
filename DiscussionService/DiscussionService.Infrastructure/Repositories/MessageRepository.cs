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
    
    public async Task<Message?> GetByIdAsync(ObjectId id, CancellationToken cancellationToken) =>
        await _collection.Find(message => message.Id == id).SingleOrDefaultAsync(cancellationToken);

    public async Task CreateAsync(Message message, CancellationToken cancellationToken) =>
        await _collection.InsertOneAsync(message, cancellationToken: cancellationToken);

    public async Task DeleteAsync(ObjectId messageId, CancellationToken cancellationToken) =>
        await _collection.DeleteOneAsync(m => m.Id == messageId,  cancellationToken);

    public async Task UpdateAsync(Message message, ObjectId messageId, CancellationToken cancellationToken) =>
        await _collection.ReplaceOneAsync(m => m.Id == messageId, message, cancellationToken: cancellationToken);
    
    public async Task<PagedResult<Message>> GetPagedAsync(Guid tweetId, PageParams pageParams, CancellationToken cancellationToken)
    {
        var elements = await _collection
            .Find(m => m.TweetId == tweetId)
            .Skip((pageParams.Page - 1) * pageParams.PageSize)
            .Limit(pageParams.PageSize)
            .ToListAsync(cancellationToken);
        var count = await _collection.CountDocumentsAsync(_ => true, cancellationToken: cancellationToken);
        return new PagedResult<Message>(elements, count);
    }

}