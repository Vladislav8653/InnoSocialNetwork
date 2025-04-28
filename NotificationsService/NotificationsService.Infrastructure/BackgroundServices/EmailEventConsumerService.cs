using System.Text.Json;
using Confluent.Kafka;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using NotificationsService.Application.Contracts.ServicesContracts;
using NotificationsService.Application.EmailService;
using NotificationsService.Application.Settings;

namespace NotificationsService.Infrastructure.BackgroundServices;

public class EmailEventConsumerService : BackgroundService
{
    private readonly ILogger<EmailEventConsumerService> _logger;
    private readonly IConsumer<Null, string> _consumer;
    private readonly ISmtpService _smtpService;
    private readonly string _topic;

    public EmailEventConsumerService(
        ILogger<EmailEventConsumerService> logger,
        ISmtpService smtpService,
        IOptions<KafkaSettings> options) 
    {
        _logger = logger;
        
        _smtpService = smtpService;
        
        var kafkaSettings = options.Value;
        var config = new ConsumerConfig
        {
            BootstrapServers = kafkaSettings.BootstrapServers,
            GroupId = kafkaSettings.GroupId,
            AutoOffsetReset = AutoOffsetReset.Earliest,
            EnableAutoCommit = true
        };

        _topic = kafkaSettings.Topic;
        _consumer = new ConsumerBuilder<Null, string>(config).Build();
    }

    protected override async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _consumer.Subscribe(_topic);
        _logger.LogInformation($"Subscribed to Kafka topic: {_topic}");

        try
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                try
                {
                    var consumeResult = _consumer.Consume(stoppingToken);
                    
                    if (consumeResult?.Message?.Value != null)
                    {
                        var emailEvent = JsonSerializer.Deserialize<SendEmailEvent>(consumeResult.Message.Value);

                        if (emailEvent != null)
                        {
                            _logger.LogInformation($"Received email event for: {emailEvent.ToEmail}");
                            await _smtpService.SendEmailAsync(
                                emailEvent.ToName,
                                emailEvent.ToEmail,
                                emailEvent.Subject,
                                emailEvent.Body
                            );
                        }
                    }
                }
                catch (ConsumeException ex)
                {
                    _logger.LogError(ex, "Kafka consume error");
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error processing email event");
                }
            }
        }
        finally
        {
            _consumer.Close();
        }
    }

    public override void Dispose()
    {
        _consumer.Dispose();
        base.Dispose();
    }
}