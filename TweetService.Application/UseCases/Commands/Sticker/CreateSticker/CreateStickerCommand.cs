using MediatR;
using TweetService.Application.DTOs.StickersDto;

namespace TweetService.Application.UseCases.Commands.Sticker.CreateSticker;

public record CreateStickerCommand : IRequest<Unit>
{
    public string? UserId {get; init; } 
    public StickerRequestDto NewSticker { get; init; }
}