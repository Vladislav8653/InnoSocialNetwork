using MediatR;
using TweetService.Application.DTOs.StickersDto;

namespace TweetService.Application.UseCases.Queries.Sticker.GetStickers;

public class GetStickersCommandHandler :  IRequestHandler<GetStickersCommand, IEnumerable<StickerResponseToDto>>
{
    public async Task<IEnumerable<StickerResponseToDto>> Handle(GetStickersCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}