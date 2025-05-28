using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Extensions.DependencyInjection;
using Moq;
using System.Text.Json;
using Grpc.Core;
using Microsoft.VisualStudio.TestPlatform.TestHost;
using NotificationsService.Application.Contracts.ServicesContracts;
using NotificationsService.Application.UseCases.HangfireHandlers;
using TweetDigest.Grpc;

namespace NotificationService.Tests;


public class TweetDigestJobIntegrationTests : IAsyncDisposable
{
    private readonly WebApplicationFactory<Program> _factory;
    private readonly Mock<TweetService.TweetServiceClient> _mockGrpcClient; 
    private readonly Mock<ISmtpService> _mockSmtpService;

    public TweetDigestJobIntegrationTests()
    {
        _mockGrpcClient = new Mock<TweetService.TweetServiceClient>();
        _mockSmtpService = new Mock<ISmtpService>();

        _factory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureServices(services =>
                {
                    services.AddSingleton(_mockGrpcClient.Object);
                    services.AddSingleton(_mockSmtpService.Object);
                    services.AddScoped<TweetDigestJob>(); 
                });
            });
    }

    public async ValueTask DisposeAsync()
    {
        await _factory.DisposeAsync();
    }

    [Fact]
    public async Task TweetDigestJob_ShouldFetchTweetsAndSendEmail()
    {
        // Arrange
        var scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        
        var job = scope.ServiceProvider.GetRequiredService<TweetDigestJob>();

        var fakeTweets = new List<DigestItem>
        {
            new DigestItem { TweetId = "1", Author = "Author1", Content = "Content1", CreatedAt = DateTime.UtcNow.ToString("o") },
            new DigestItem { TweetId = "2", Author = "Author2", Content = "Content2", CreatedAt = DateTime.UtcNow.ToString("o") }
        };
        var digestResponse = new DigestResponse();
        digestResponse.Items.AddRange(fakeTweets);
        
        _mockGrpcClient
            .Setup(c => c.GetDailyDigestAsync(
                It.IsAny<DigestRequest>(), // Можно уточнить параметры запроса
                null, null, It.IsAny<CancellationToken>())) // CallOptions, CancellationToken
            .Returns(new AsyncUnaryCall<DigestResponse>(
                Task.FromResult(digestResponse),
                Task.FromResult(new Metadata()),
                () => Status.DefaultSuccess,
                () => new Metadata(),
                () => { }
            ));

        var expectedJsonBody = JsonSerializer.Serialize(fakeTweets);

        // Act
        await job.ExecuteAsync(CancellationToken.None);

        // Assert
        _mockGrpcClient.Verify(c => c.GetDailyDigestAsync(
            It.Is<DigestRequest>(r =>
                !string.IsNullOrEmpty(r.From) && !string.IsNullOrEmpty(r.To)), 
            null, null, It.IsAny<CancellationToken>()), Times.Once);

        _mockSmtpService.Verify(s => s.SendEmailAsync(
            "Users",
            "sashamelnikov952@gmail.com",
            "Tweet digest",
            expectedJsonBody), Times.Once);
    }

    [Fact]
    public async Task TweetDigestJob_ShouldNotSendEmail_WhenNoTweets()
    {
        // Arrange
        var scopeFactory = _factory.Services.GetRequiredService<IServiceScopeFactory>();
        using var scope = scopeFactory.CreateScope();
        var job = scope.ServiceProvider.GetRequiredService<TweetDigestJob>();

        var emptyDigestResponse = new DigestResponse(); // Пустой список твитов

        _mockGrpcClient
            .Setup(c => c.GetDailyDigestAsync(
                It.IsAny<DigestRequest>(),
                null, null, It.IsAny<CancellationToken>()))
            .Returns(new AsyncUnaryCall<DigestResponse>(
                Task.FromResult(emptyDigestResponse),
                Task.FromResult(new Metadata()),
                () => Status.DefaultSuccess,
                () => new Metadata(),
                () => { }
            ));

        // Act
        await job.ExecuteAsync(CancellationToken.None);

        // Assert
        _mockSmtpService.Verify(s => s.SendEmailAsync(
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>(),
            It.IsAny<string>()), Times.Never); // Email не должен отправляться
    }
}