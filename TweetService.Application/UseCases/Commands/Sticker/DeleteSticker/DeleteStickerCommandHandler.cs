using MediatR;

namespace TweetService.Application.UseCases.Commands.Sticker.DeleteSticker;

public class DeleteStickerCommandHandler : IRequestHandler<DeleteStickerCommand, Unit>
{
    public async Task<Unit> Handle(DeleteStickerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}