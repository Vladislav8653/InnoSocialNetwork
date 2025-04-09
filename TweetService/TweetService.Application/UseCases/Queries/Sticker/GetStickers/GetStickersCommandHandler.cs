using AutoMapper;
using MediatR;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.DTOs.StickersDto;
using TweetService.Application.Pagination;

namespace TweetService.Application.UseCases.Queries.Sticker.GetStickers;

public class GetStickersCommandHandler(
    IStickerRepository stickerRepository,
    IMapper mapper) :
    IRequestHandler<GetStickersCommand, PagedResult<StickerResponseDto>>
{
    public async Task<PagedResult<StickerResponseDto>> Handle(GetStickersCommand request, CancellationToken cancellationToken)
    {
        var stickers = await stickerRepository.GetByPageAsync(
            request.PageParams, false, cancellationToken);
        
        var stickersResponseDto = mapper.Map<IEnumerable<StickerResponseDto>>(stickers.Items);
        
        return new PagedResult<StickerResponseDto>(stickersResponseDto, stickers.Total);
    }
}