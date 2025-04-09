using MediatR;
using TweetService.Application.DTOs.StickersDto;
using TweetService.Application.Pagination;

namespace TweetService.Application.UseCases.Queries.Sticker.GetStickers;

public record GetStickersCommand : IRequest<PagedResult<StickerResponseDto>>
{
    public PageParams PageParams { get; init; } = null!;
}