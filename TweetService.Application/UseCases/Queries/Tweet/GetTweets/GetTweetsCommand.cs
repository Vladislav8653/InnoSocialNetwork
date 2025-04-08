using MediatR;
using TweetService.Application.DTOs.TweetsDto;
using TweetService.Application.Pagination;

namespace TweetService.Application.UseCases.Queries.Tweet.GetTweets;

public record GetTweetsCommand : IRequest<PagedResult<TweetResponseToDto>>
{
    public PageParams PageParams { get; init; } = null!;
}