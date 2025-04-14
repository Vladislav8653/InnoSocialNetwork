using MediatR;
using TweetService.Application.UseCases.Commands.Sticker.UpdateSticker;

namespace TweetService.Application.UseCases.Commands.Tweet.UpdateTweet;

public class UpdateTweetCommandHandler : IRequestHandler<UpdateStickerCommand, Unit>
{
    public async Task<Unit> Handle(UpdateStickerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}