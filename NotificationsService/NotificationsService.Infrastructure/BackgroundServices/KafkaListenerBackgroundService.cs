using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using NotificationsService.Application.Contracts;
using NotificationsService.Application.DTOs;
using NotificationsService.Application.EmailService;

namespace NotificationsService.Infrastructure.BackgroundServices;

public class KafkaListenerBackgroundService(
    IConsumer<string, string> consumer,
    IServiceProvider serviceProvider
    ) : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        consumer.Subscribe([
            "notification.email",
            "notification.in-app",
            "tweet.created"
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
                    break;
                }
                case "notification.in-app":
                {
                    break;
                }
                case "tweet.created":
                {
                    var message = JsonSerializer.Deserialize<TweetDigestEvent>(consumeResult.Message.Value);
                    if (message is null)
                        throw new JsonException("Tweet message could not be deserialized");

                    var handler = scopedProvider.GetRequiredService<IEventHandler<TweetDigestEvent>>();
                    await handler.HandleAsync(message, stoppingToken);
                    break;
                }
            }
        }
    }
}