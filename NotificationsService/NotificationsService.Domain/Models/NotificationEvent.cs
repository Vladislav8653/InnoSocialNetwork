using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using NotificationsService.Domain.Enums;

namespace NotificationsService.Domain.Models;

public class NotificationEvent
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } 
    public string HangfireJobId { get; set; } = default!;
    public string EventType { get; set; } = default!;
    public string Source { get; set; } = default!;          
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public IEnumerable<Guid> TargetUsers { get; set; } = [];  
    
    public string Title { get; set; } = default!;         // Заголовок уведомления
    public string Body { get; set; } = default!;          // Основной текст
    public NotificationChannel Channel { get; set; }   // Куда отправлять: "email", "in_app", "push"
}
