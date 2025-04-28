using MediatR;

namespace TweetService.Application.UseCases.Commands.Sticker.DeleteSticker;

public record DeleteStickerCommand : IRequest<Unit>
{
    public string? UserId {get; init; } 
    public Guid StickerId {get; init;}
}