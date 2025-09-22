using NotificationsService.Application.Contracts.Grpc;
using NotificationsService.Application.DTOs.DigestDto;
using TweetDigest.Grpc;

namespace NotificationsService.Infrastructure.Grpc;

public class TweetDigestGrpcClient(TweetService.TweetServiceClient client) : ITweetDigestGrpcClient
{
    public async Task<List<TweetDigestItemDto>> GetDigestAsync(DateTime from, DateTime to)
    {
        var request = new DigestRequest
        {
            From = from.ToString("o"),
            To = to.ToString("o")
        };

        var response = await client.GetDailyDigestAsync(request);

        return response.Items.Select(item => new TweetDigestItemDto
        {
            TweetId = item.TweetId,
            Content = item.Content,
            CreatedAt = DateTime.TryParse(item.CreatedAt, out var createdAt) ? createdAt : default,
            ModifiedAt = DateTime.TryParse(item.ModifiedAt, out var modifiedAt) ? modifiedAt : default,
        }).ToList();
    }
}
