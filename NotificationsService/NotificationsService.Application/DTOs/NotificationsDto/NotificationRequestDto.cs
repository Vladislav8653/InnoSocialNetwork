using System.ComponentModel.DataAnnotations;
using NotificationsService.Domain.Enums;

namespace NotificationsService.Application.DTOs.NotificationsDto;

public record NotificationRequestDto
{
    public string EventType { get; init; } = default!;
    public string Source { get; init; } = default!;          
    
    public DateTime Timestamp { get; init; } = DateTime.UtcNow;
    public IEnumerable<Guid> TargetUsers { get; init; } = [];  
    
    public string Title { get; init; } = default!;         // Заголовок уведомления
    public string Body { get; init; } = default!;          // Основной текст
    
    [EnumDataType(typeof(NotificationChannel))]
    public string Channel { get; init; } = default!;   // Куда отправлять: "email", "in_app", "push"
}