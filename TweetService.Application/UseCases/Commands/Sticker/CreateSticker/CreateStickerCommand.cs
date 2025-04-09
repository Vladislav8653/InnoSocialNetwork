using MediatR;
using TweetService.Application.DTOs.StickersDto;

namespace TweetService.Application.UseCases.Commands.Sticker.CreateSticker;

public record CreateStickerCommand : IRequest<Unit>
{
    public StickerRequestDto StickerDto { get; init; }
}