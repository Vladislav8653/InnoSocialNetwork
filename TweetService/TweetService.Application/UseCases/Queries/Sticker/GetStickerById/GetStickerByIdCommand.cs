using MediatR;
using TweetService.Application.DTOs.StickersDto;

namespace TweetService.Application.UseCases.Queries.Sticker.GetStickerById;

public record GetStickerByIdCommand : IRequest<StickerResponseDto>
{
    public Guid Id { get; init; }
}