using FluentValidation;
using MediatR;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.Exceptions;

namespace TweetService.Application.UseCases.Commands.Sticker.DeleteSticker;

public class DeleteStickerCommandHandler(
    IStickerRepository stickerRepository) : 
    IRequestHandler<DeleteStickerCommand, Unit>
{
    public async Task<Unit> Handle(DeleteStickerCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UserId, out var userIdGuid))
        {
            throw new ValidationException("UserId is invalid");
        }
        
        var stickers = await stickerRepository
            .FindByConditionAsync(sticker => sticker.Id == request.StickerId,
                false, cancellationToken);
        var sticker = stickers.FirstOrDefault();
        if (sticker is null)
            throw new NotFoundException($"Sticker with id {request.StickerId} not found");
        
        if (sticker.UserId != userIdGuid)
        {
            throw new UnauthorizedAccessException("User is not authorized to delete this sticker");
        }
        
        await stickerRepository.DeleteAsync(sticker, cancellationToken);
        
        return Unit.Value;
    }
}