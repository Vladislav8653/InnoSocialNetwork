using System.Text.Json;
using AutoFixture;
using Confluent.Kafka;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using Moq;
using NotificationsService.Application.Contracts.ServicesContracts;
using NotificationsService.Application.DTOs;

namespace NotificationService.Tests;

public class KafkaListenerIntegrationTests : IAsyncLifetime
{
    private WebApplicationFactory<Program> _factory;
    private Mock<IConsumer<string, string>> _mockKafkaConsumer;
    private Mock<ISmtpService> _mockSmtpService;
    private CancellationTokenSource _cts;
    private IHost _host;
    private IFixture _fixture;
    
    public async Task InitializeAsync()
    {
        _fixture = new Fixture();
        _mockKafkaConsumer = new Mock<IConsumer<string, string>>();
        _mockSmtpService = new Mock<ISmtpService>();
        _cts = new CancellationTokenSource();

        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_mockKafkaConsumer.Object);
                    services.AddSingleton(_mockSmtpService.Object);
                });
            });
        
        _host = _factory.Server.Services.GetRequiredService<IHost>();

        await Task.Delay(TimeSpan.FromSeconds(3));  
    }

    public async Task DisposeAsync()
    {
        _cts.Cancel();
         await _host.StopAsync(); 
        _host?.Dispose();
        _factory?.Dispose();
        _cts?.Dispose();
    }

    [Fact]
    public async Task KafkaListener_ShouldProcessEmailEvent_AndCallSmtpService()
    {
        //Arrange
        var emailEvent = _fixture.Create<SendEmailEvent>();
        var messageJson = JsonSerializer.Serialize(emailEvent);
        
        var consumeResult = new ConsumeResult<string, string>
        {
            Message = new Message<string, string> { Value = messageJson },
            Topic = "notification.email",
            Offset = 0,
            Partition = 0
        };

        var messageProcessedEvent = new ManualResetEventSlim(false);

        _mockSmtpService.Setup(s => s.SendEmailAsync(
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>(),
                It.IsAny<string>()))
            .Callback<string, string, string, string>((name, toEmail, subject, body) =>
            {
                messageProcessedEvent.Set();
            })
            .Returns(Task.CompletedTask);
        
        _mockKafkaConsumer.
            Setup(c => c.Consume(It.IsAny<CancellationToken>()))
            .Returns(() =>
            {
                if (!messageProcessedEvent.IsSet)
                {
                    return consumeResult;
                }
                _cts.Cancel();
                throw new OperationCanceledException(_cts.Token);
            });    
        
        //Act
        var processedInTime = messageProcessedEvent.Wait(TimeSpan.FromSeconds(10));
        
        //Assert
        Assert.True(processedInTime, "\"SmtpService.SendEmailAsync was not called within the timeout");
        _mockSmtpService.Verify(s => s.SendEmailAsync(
            emailEvent.ToName,
            emailEvent.ToEmail,
            emailEvent.Subject,
            emailEvent.Body), Times.Once);
        
        _mockKafkaConsumer.Verify(c => c.Subscribe(It.Is<List<string>> (topics =>
            topics.Contains("notification.email") && topics.Contains("notification.in-app"))), Times.Once);
    }
}