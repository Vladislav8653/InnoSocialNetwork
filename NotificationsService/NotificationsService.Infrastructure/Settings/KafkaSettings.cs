namespace NotificationsService.Infrastructure.Settings;

public class KafkaSettings
{
    public string BootstrapServers { get; set; }
    public string GroupId { get; set; }
}