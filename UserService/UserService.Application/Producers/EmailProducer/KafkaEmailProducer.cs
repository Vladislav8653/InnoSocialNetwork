using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Options;
using UserService.Application.Settings;

namespace UserService.Application.Producers.EmailProducer;

public class KafkaEmailProducer
{
    private readonly IProducer<Null, string> _producer;
    private readonly string _topic;

    public KafkaEmailProducer(IOptions<KafkaSettings> options)
    {
        var settings = options.Value;
        var config = new ProducerConfig
        {
            BootstrapServers = settings.BootstrapServers
        };

        _topic = settings.Topic;

        _producer = new ProducerBuilder<Null, string>(config).Build();
    }

    public async Task SendEmailAsync(SendEmailEvent emailEvent)
    {
        var message = new Message<Null, string>
        {
            Value = JsonSerializer.Serialize(emailEvent)
        };

        await _producer.ProduceAsync(_topic, message);
    }
}
