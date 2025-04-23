using NotificationsService.Application.Contracts.RepositoryContracts;
using NotificationsService.Domain.Models;

namespace NotificationsService.Infrastructure.Repositories;

using MongoDB.Driver;

public class NotificationRepository : INotificationRepository
{
    private readonly IMongoCollection<Notification> _collection;

    public NotificationRepository(IMongoDatabase database)
    {
        _collection = database.GetCollection<Notification>("notifications");
    }

    public async Task<Notification?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<Notification>.Filter.Eq(n => n.Id, id);
        return await _collection.Find(filter).FirstOrDefaultAsync(cancellationToken);
    }

    public async Task CreateAsync(Notification notification, CancellationToken cancellationToken)
    {
        await _collection.InsertOneAsync(notification, null, cancellationToken);
    }

    public async Task DeleteAsync(Guid id, CancellationToken cancellationToken)
    {
        var filter = Builders<Notification>.Filter.Eq(n => n.Id, id);
        await _collection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task UpdateHangfireJobIdAsync(Guid id, string jobId, CancellationToken cancellationToken)
    {
        var filter = Builders<Notification>.Filter.Eq(n => n.Id, id);
        var update = Builders<Notification>.Update.Set(n => n.HangfireJobId, jobId);
        await _collection.UpdateOneAsync(filter, update, cancellationToken: cancellationToken);
    }
}
