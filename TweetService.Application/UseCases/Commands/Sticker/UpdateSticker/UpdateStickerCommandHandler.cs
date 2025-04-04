using MediatR;

namespace TweetService.Application.UseCases.Commands.Sticker.UpdateSticker;

public class UpdateStickerCommandHandler : IRequestHandler<UpdateStickerCommand, Unit>
{
    public async Task<Unit> Handle(UpdateStickerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}