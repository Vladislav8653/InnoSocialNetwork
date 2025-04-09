using MediatR;
using TweetService.Application.DTOs.StickersDto;

namespace TweetService.Application.UseCases.Queries.Sticker.GetStickers;

public class GetStickersCommandHandler :  IRequestHandler<GetStickersCommand, IEnumerable<StickerResponseDto>>
{
    public async Task<IEnumerable<StickerResponseDto>> Handle(GetStickersCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}