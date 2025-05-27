using Newtonsoft.Json;
using NotificationsService.Application.Contracts.ServicesContracts;
using NotificationsService.Application.GRPC;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace NotificationsService.Application.UseCases.HangfireHandlers;

public class TweetDigestJob(TweetDigestGrpcClient tweetClient, ISmtpService emailService)
{
    public async Task ExecuteAsync(CancellationToken stoppingToken)
    {
        var from = DateTime.UtcNow.AddDays(-1);
        var to = DateTime.UtcNow;

        var tweets = await tweetClient.GetDigestAsync(from, to);

        if (!tweets.Any())
        {
            return;
        }
        
        var digestJson = JsonSerializer.Serialize(tweets);

        await emailService.SendEmailAsync(
            "Users",
            "sashamelnikov952@gmail.com",
            "Tweet digest",
            digestJson);
    }
}
