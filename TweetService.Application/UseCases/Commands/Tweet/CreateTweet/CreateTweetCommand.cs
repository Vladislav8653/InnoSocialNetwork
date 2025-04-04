using MediatR;
using TweetService.Application.DTOs.TweetsDto;

namespace TweetService.Application.UseCases.Commands.Tweet.CreateTweet;

public record CreateTweetCommand : IRequest<Unit>
{
    public TweetRequestToDto TweetRequestToDto { get; init; }
}