using System.Text.Json;
using Confluent.Kafka;
using Confluent.Kafka.Admin;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationsService.Application.Contracts;
using NotificationsService.Application.DTOs;

namespace NotificationsService.Infrastructure.BackgroundServices;

public class KafkaListenerBackgroundService(
    IConsumer<string, string> consumer,
    IServiceProvider serviceProvider,
    IAdminClient adminClient
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        /*var topicSpecs = new List<TopicSpecification>
        {
            new() { Name = "notification.email" },
            new() { Name = "notification.in-app" },
        };

        await adminClient.CreateTopicsAsync(topicSpecs);*/
        
        consumer.Subscribe([
            "notification.email",
            "notification.in-app"
        ]);

        while (!stoppingToken.IsCancellationRequested)
        {
            var consumeResult = consumer.Consume(stoppingToken);

            using var scope = serviceProvider.CreateScope();
            var scopedProvider = scope.ServiceProvider;
            
            switch (consumeResult.Topic)
            {
                case "notification.email":
                {
                    var message = JsonSerializer.Deserialize<SendEmailEvent>(consumeResult.Message.Value);
                    if (message is null)
                        throw new JsonException("Email message could not be deserialized");
                    var handler = scopedProvider.GetRequiredService<IEventHandler<SendEmailEvent>>();
                    await handler.HandleAsync(message, stoppingToken);
                    consumer.Commit(consumeResult);
                    break;
                }
                case "notification.in-app":
                {
                    break;
                }
            }
        }
    }
}