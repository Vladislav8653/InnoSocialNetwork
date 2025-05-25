using System.Text.Json;
using AutoFixture;
using Confluent.Kafka;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Moq;
using UserService.Application.Contracts;
using UserService.Application.DTO;
using UserService.Application.DTO.PasswordResetDto;
using UserService.Application.Settings;
using UserService.Application.UseCases.Commands.ResetUserCommands.SendResetEmail;
using UserService.Domain.Models;
using UserService.Infrastructure.Producers.EmailProducer;

namespace UserService.Tests.UserResetTests;

public class SendResetEmailCommandHandlerTests : IAsyncLifetime
{
    private readonly IConsumer<Ignore, string> _consumer;
    private readonly INotificationService _kafkaNotificationService;
    private readonly UserManager<User> _userManager;
    private readonly Fixture _fixture;
    
    public SendResetEmailCommandHandlerTests()
    {
        _fixture = new Fixture();
        var kafkaSettings = new KafkaSettings
        {
            BootstrapServers = "localhost:9092",
            Topic = new List<string>{"notification.email"}
        };
        
        _kafkaNotificationService = new KafkaEmailProducer(Options.Create(kafkaSettings));

        var consumerConfig = new ConsumerConfig
        {
            GroupId = "1",
            BootstrapServers = kafkaSettings.BootstrapServers,
            AutoOffsetReset = AutoOffsetReset.Earliest,
        };
        
        _consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
        _consumer.Subscribe(kafkaSettings.Topic);
        
        // Создание fake UserManager
        var store = new Mock<IUserStore<User>>();
        _userManager = MockUserManager.CreateMock(store.Object);
    }
    
    [Fact]
    public async Task Handle_SendsEmailEventToKafka()
    {
        // Arrange
        var testUser = _fixture.Build<User>()
            .With(x => x.UserName, "MockUser")
            .Create();
            

        await _userManager.CreateAsync(testUser, _fixture.Create<string>());

        var dto = new EmailForPasswordResetDto { Email = testUser.Email };
        var handler = new SendResetEmailCommandHandler(_userManager, _kafkaNotificationService);

        // Act
        await handler.Handle(new SendResetEmailCommand { NewPasswordDto = dto }, CancellationToken.None);

        // Assert
        var consumeResult = _consumer.Consume(TimeSpan.FromSeconds(5));
        Assert.NotNull(consumeResult);

        var message = JsonSerializer.Deserialize<EmailDto>(consumeResult.Message.Value);
        Assert.Equal("Reset Password", message.Subject);
        Assert.Equal(testUser.Email, message.ToEmail);
        Assert.Equal(testUser.UserName, message.ToName);
        Assert.Contains("Your reset token:", message.Body);
    }
    
    public Task InitializeAsync() => Task.CompletedTask;

    public Task DisposeAsync()
    {
        _consumer?.Close();
        _consumer?.Dispose();
        return Task.CompletedTask;
    }
}


public static class MockUserManager
{
    public static UserManager<User> CreateMock(IUserStore<User> store)
    {
        var mgr = new Mock<UserManager<User>>(store, null, null, null, null, null, null, null, null);

        mgr.Setup(x => x.FindByEmailAsync(It.IsAny<string>()))
            .ReturnsAsync((string email) => new User { Email = email, UserName = "MockUser" });

        mgr.Setup(x => x.GeneratePasswordResetTokenAsync(It.IsAny<User>()))
            .ReturnsAsync("mock-reset-token");

        mgr.Setup(x => x.CreateAsync(It.IsAny<User>(), It.IsAny<string>()))
            .ReturnsAsync(IdentityResult.Success);

        return mgr.Object;
    }
}
