using AutoMapper;
using MediatR;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.DTOs.StickersDto;
using TweetService.Application.Exceptions;

namespace TweetService.Application.UseCases.Queries.Sticker.GetStickerById;

public class GetStickerByIdCommandHandler(
    IStickerRepository stickerRepository,
    IMapper mapper) : 
    IRequestHandler<GetStickerByIdCommand, StickerResponseDto>
{
    public async Task<StickerResponseDto> Handle(GetStickerByIdCommand request, CancellationToken cancellationToken)
    {
        var stickers = await stickerRepository.FindByConditionAsync(
            sticker => sticker.Id == request.Id,false, cancellationToken);
        var sticker = stickers.FirstOrDefault();
        if (sticker is null)
            throw new NotFoundException($"Sticker with id {request.Id} not found");
        
        var stickerResponseDto = mapper.Map<StickerResponseDto>(sticker);
        
        return stickerResponseDto;
    }
}