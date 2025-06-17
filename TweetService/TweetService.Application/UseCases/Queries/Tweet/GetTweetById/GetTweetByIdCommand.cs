using MediatR;
using TweetService.Application.DTOs.TweetsDto;

namespace TweetService.Application.UseCases.Queries.Tweet.GetTweetById;

public record GetTweetByIdCommand : IRequest<TweetResponseDto>
{
    public Guid Id { get; init; }
}