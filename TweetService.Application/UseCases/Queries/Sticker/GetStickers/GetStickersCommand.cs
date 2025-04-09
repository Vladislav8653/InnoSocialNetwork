using MediatR;
using TweetService.Application.DTOs.StickersDto;

namespace TweetService.Application.UseCases.Queries.Sticker.GetStickers;

public record GetStickersCommand : IRequest<IEnumerable<StickerResponseDto>>;