using System.Text.Json;
using Confluent.Kafka;
using DiscussionService.Infrastructure.Settings;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace DiscussionService.Infrastructure.Consumers;

public class TweetDeletedConsumer(IOptions<KafkaSettings> kafkaSettings) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaSettings.Value.BootstrapServers,
            GroupId = "discussion-service",
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
        
        using var consumer = new ConsumerBuilder<Ignore, string>(config).Build();
        consumer.Subscribe(kafkaSettings.Value.TweetDeleteTopic);

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                var consumeResult = consumer.Consume(stoppingToken);

                var messageJson = consumeResult.Message.Value;

                var data = JsonSerializer.Deserialize<Dictionary<string, object>>(messageJson);

                if (data != null && data.TryGetValue("tweet_id", out var tweetIdRaw)) // must get rid of string literals
                {
                    if (!Guid.TryParse(tweetIdRaw.ToString(), out var tweetId))
                    {
                        throw new JsonException("Failed to deserialize tweet id");
                    }
                
                    // delete all message by tweet id
                }
            }
        }
        finally
        {
            consumer.Close();
        }
    }
}