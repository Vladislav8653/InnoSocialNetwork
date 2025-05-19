using Confluent.Kafka;
using Microsoft.Extensions.Hosting;

namespace NotificationsService.Infrastructure.BackgroundServices;

public class KafkaListenerBackgroundService(
    IConsumer<string, string> consumer
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        consumer.Subscribe([
            "notification.email",
            "notification.in-app"
        ]);

        while (!stoppingToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(stoppingToken);

            switch (consumeResult.Topic)
            {
                case "notification.email":
                {
                    break;
                }
                case "in-app":
                {
                    break;
                }
            }
        }
    }
}