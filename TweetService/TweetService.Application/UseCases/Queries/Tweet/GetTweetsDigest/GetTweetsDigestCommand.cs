using MediatR;
using TweetService.Application.DTOs.TweetsDto;

namespace TweetService.Application.UseCases.Queries.Tweet.GetTweetsDigest;

public record GetTweetsDigestCommand : IRequest<IEnumerable<TweetDigestDto>>
{
    public DateTime StartDate { get; init; }
    public DateTime EndDate { get; init; }
}