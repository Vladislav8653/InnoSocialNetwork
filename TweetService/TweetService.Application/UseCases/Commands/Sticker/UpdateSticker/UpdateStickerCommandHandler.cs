using AutoMapper;
using FluentValidation;
using MediatR;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.Exceptions;

namespace TweetService.Application.UseCases.Commands.Sticker.UpdateSticker;

public class UpdateStickerCommandHandler(
    IStickerRepository stickerRepository, 
    IMapper mapper,
    IValidator<Domain.Models.Sticker> validator) : 
    IRequestHandler<UpdateStickerCommand, Unit>
{
    public async Task<Unit> Handle(UpdateStickerCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UserId, out var userIdGuid))
        {
            throw new ValidationException("UserId is invalid");
        }
        
        var stickers = await stickerRepository.FindByConditionAsync(sticker => 
            sticker.Id == request.StickerId,false, cancellationToken);
        var sticker = stickers.FirstOrDefault();
        if (sticker is null)
            throw new NotFoundException($"Sticker with id {request.StickerId} not found");
        
        if (sticker.UserId != userIdGuid)
        {
            throw new UnauthorizedAccessException("User is not authorized to update this sticker");
        }
            
        mapper.Map(request.NewSticker, sticker);
        
        var validationResult = await validator.ValidateAsync(sticker, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        await stickerRepository.UpdateAsync(sticker, cancellationToken);
        
        return Unit.Value;
    }
}