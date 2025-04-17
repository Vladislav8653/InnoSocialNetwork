using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using TweetService.Application.Contracts.ProducerContracts;
using TweetService.Infrastructure.Settings;

namespace TweetService.Infrastructure.Producers;

public class TweetDeletedProducer : ITweetDeletedProducer
{
    private readonly IProducer<string, string> _producer;
    private readonly string _topic;
    
    public TweetDeletedProducer(IOptions<KafkaSettings> kafkaSettings)
    {
        var config = new ProducerConfig
        {
            BootstrapServers = kafkaSettings.Value.BootstrapServers
        };
        
        _producer = new ProducerBuilder<string, string>(config).Build();
        _topic = kafkaSettings.Value.TweetDeleteTopic; 
    }
    
    public async Task PublishTweetDeletedEvent(Guid tweetId)
    {
        var message = new
        {
            tweet_id = tweetId,
            event_type = "tweet_deleted"
        };
        
        var messageJson = JsonSerializer.Serialize(message);

        var kafkaMessage = new Message<string, string>
        {
            Key = tweetId.ToString(),
            Value = messageJson,
        };
        
        await _producer.ProduceAsync(_topic, kafkaMessage);
    }
}