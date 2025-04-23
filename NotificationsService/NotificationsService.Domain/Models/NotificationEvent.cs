namespace NotificationsService.Domain.Models;

public class NotificationEvent
{
    public string EventType { get; set; } = default!;
    public string Source { get; set; } = default!;          
    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
    public IEnumerable<Guid> TargetUsers { get; set; } = [];  
    public NotificationPayload Notification { get; set; } = default!;
}
