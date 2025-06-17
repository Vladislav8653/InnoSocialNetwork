using MediatR;

namespace TweetService.Application.UseCases.Commands.Tweet.DeleteTweet;

public record DeleteTweetCommand : IRequest<Unit>
{
    public string? UserId {get; init; } 
    public Guid TweetId {get; init;}
}