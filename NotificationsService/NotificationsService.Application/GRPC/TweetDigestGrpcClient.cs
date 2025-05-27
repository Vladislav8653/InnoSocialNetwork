using Tweet.Grpc;

namespace NotificationsService.Application.GRPC;

public class TweetDigestGrpcClient(TweetService.TweetServiceClient client)
{
    public async Task<List<DigestItem>> GetDigestAsync(DateTime from, DateTime to)
    {
        var request = new DigestRequest
        {
            From = from.ToString("o"),
            To = to.ToString("o")
        };

        var response = await client.GetDailyDigestAsync(request);
        
        return response.Items.ToList();
    }
}
