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
}