namespace DiscussionService.Infrastructure.Settings;

public record KafkaSettings
{
    public string BootstrapServers { get; init; }
    public string TweetDeleteTopic { get; init; }
}