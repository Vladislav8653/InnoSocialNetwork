using MediatR;

namespace TweetService.Application.UseCases.Commands.Sticker.DeleteSticker;

public record DeleteStickerCommand : IRequest<Unit>
{
    public Guid Id {get; init;}
}