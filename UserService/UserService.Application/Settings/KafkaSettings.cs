namespace UserService.Application.Settings;

public record KafkaSettings
{
    public string BootstrapServers { get; init; }
    public string GroupId { get; init; }
    public ICollection<string> Topic { get; init; }
}