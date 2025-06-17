using MediatR;
using TweetService.Application.DTOs.TweetsDto;

namespace TweetService.Application.UseCases.Commands.Tweet.UpdateTweet;

public record UpdateTweetCommand : IRequest<Unit>
{
    public string? UserId {get; init; } 
    public Guid TweetId { get; init; }
    public TweetRequestDto NewTweet { get; init; }
}