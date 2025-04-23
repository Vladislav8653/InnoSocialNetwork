using NotificationsService.Domain.Enums;

namespace NotificationsService.Domain.Models;

public class NotificationPayload
{
    public string Title { get; set; } = default!;         // Заголовок уведомления
    public string Body { get; set; } = default!;          // Основной текст
    public string Type { get; set; } = default!;          // Тип (например: tweet_created, comment_added)
    public NotificationChannels Channels { get; set; }   // Куда отправлять: "email", "in_app", "push"
}
