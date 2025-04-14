using MediatR;
using TweetService.Application.DTOs.TweetsDto;

namespace TweetService.Application.UseCases.Commands.Tweet.UpdateTweet;

public record UpdateTweetCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public TweetRequestToDto TweetRequestToDto { get; init; }
}