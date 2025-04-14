using MediatR;

namespace TweetService.Application.UseCases.Commands.Sticker.CreateSticker;

public class CreateStickerCommandHandler : IRequestHandler<CreateStickerCommand, Unit>
{
    public async Task<Unit> Handle(CreateStickerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}