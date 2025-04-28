namespace NotificationsService.Application.EmailService;

public class SendEmailEvent
{
    public string ToEmail { get; set; }
    public string ToName { get; set; }
    public string Subject { get; set; }
    public string Body { get; set; }
}
