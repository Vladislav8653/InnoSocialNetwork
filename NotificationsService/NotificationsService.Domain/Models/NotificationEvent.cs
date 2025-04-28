using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotificationsService.Domain.Models;

public class NotificationEvent
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } 
    public string HangfireJobId { get; set; }
    public string EventType { get; set; } = default!;
    public string Source { get; set; } = default!;          
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public IEnumerable<Guid> TargetUsers { get; set; } = [];  
    public NotificationPayload Payload { get; set; } = default!;
}
