using MediatR;
using TweetService.Application.DTOs.TweetsDto;

namespace TweetService.Application.UseCases.Commands.Tweet.CreateTweet;

public record CreateTweetCommand : IRequest<Unit>
{
    public TweetRequestDto NewTweet { get; init; }
    public string? UserId {get; init; } 
}