using MediatR;
using TweetService.Application.DTOs.StickersDto;

namespace TweetService.Application.UseCases.Queries.Sticker.GetStickerById;

public class GetStickerByIdCommandHandler : IRequestHandler<GetStickerByIdCommand, StickerResponseToDto>
{
    public async Task<StickerResponseToDto> Handle(GetStickerByIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}