using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace NotificationsService.Domain.Models;

public class Notification
{
    [BsonId]
    [BsonRepresentation(BsonType.String)]
    public Guid Id { get; set; } 
    
    public string EventType { get; set; } 
    
    public string Source { get; set; } 
    
    public DateTime CreatedAt { get; set; } 
    public string HangfireJobId { get; set; }
    public int MinutesBeforeDeadline { get; set; }
    public DateTime ReminderTime => Deadline.AddMinutes(-MinutesBeforeDeadline);
    [BsonDateTimeOptions(Kind = DateTimeKind.Utc)] 
    public DateTime Deadline { get; set; } 
}