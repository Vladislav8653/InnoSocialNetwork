using MediatR;
using TweetService.Application.DTOs.StickersDto;

namespace TweetService.Application.UseCases.Commands.Sticker.UpdateSticker;

public record UpdateStickerCommand : IRequest<Unit>
{
    public Guid Id { get; init; }
    public StickerRequestToDto StickerRequestToDto { get; init; }
}