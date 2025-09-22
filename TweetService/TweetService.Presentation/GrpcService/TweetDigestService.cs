using Grpc.Core;
using MediatR;
using TweetDigest.Grpc;
using TweetService.Application.UseCases.Queries.Tweet.GetTweetsDigest;

namespace TweetService.Presentation.GrpcService;

public class TweetDigestService(ISender sender) : TweetDigest.Grpc.TweetService.TweetServiceBase
{
    public override async Task<DigestResponse> GetDailyDigest(DigestRequest request, ServerCallContext context)
    {
        var query = new GetTweetsDigestCommand
        {
            StartDate =  DateTime.TryParse(request.From, out var startDate) ? startDate.ToUniversalTime() : default,
            EndDate = DateTime.TryParse(request.To, out var endDate) ? endDate.ToUniversalTime() : default,
        };
        
        var tweetsDigest = await sender.Send(query, context.CancellationToken);

        var a = tweetsDigest.Select(item => new DigestItem
        {
            TweetId = item.Id.ToString(),
            Content = item.Content,
            CreatedAt = item.Created.ToString("o"),
            ModifiedAt = item.Modified.ToString("o"),
        });
        
        var response = new DigestResponse();
        response.Items.Add(a);
        return response;
    }
}