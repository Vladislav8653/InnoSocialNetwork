using MediatR;
using TweetService.Application.DTOs.TweetsDto;
using TweetService.Application.Pagination;

namespace TweetService.Application.UseCases.Queries.Tweet.GetTweets;

public record GetTweetsCommand : IRequest<PagedResult<TweetResponseDto>>
{
    public PageParams PageParams { get; init; } = null!;
}