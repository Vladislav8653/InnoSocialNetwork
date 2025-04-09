using MediatR;
using TweetService.Application.DTOs.StickersDto;

namespace TweetService.Application.UseCases.Commands.Sticker.UpdateSticker;

public record UpdateStickerCommand : IRequest<Unit>
{
    public string? UserId {get; init; } 
    public Guid StickerId { get; init; }
    public StickerRequestDto NewSticker { get; init; }
}