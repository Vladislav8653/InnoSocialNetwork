using MediatR;

namespace TweetService.Application.UseCases.Commands.Tweet.DeleteTweet;

public record DeleteStickerCommand : IRequest<Unit>
{
    public Guid Id {get; init;}
}