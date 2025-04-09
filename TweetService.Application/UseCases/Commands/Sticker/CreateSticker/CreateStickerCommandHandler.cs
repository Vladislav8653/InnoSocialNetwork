using AutoMapper;
using FluentValidation;
using MediatR;
using TweetService.Application.Contracts.RepositoryContracts;

namespace TweetService.Application.UseCases.Commands.Sticker.CreateSticker;

public class CreateStickerCommandHandler( 
    IMapper mapper, 
    IStickerRepository stickerRepository, 
    IValidator<Domain.Models.Sticker> validator) : 
    IRequestHandler<CreateStickerCommand, Unit>
{
    public async Task<Unit> Handle(CreateStickerCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UserId, out var userIdGuid))
        {
            throw new ValidationException("UserId is invalid");
        }
        
        var sticker = mapper.Map<Domain.Models.Sticker>(request.NewSticker);
        sticker.UserId = userIdGuid;
        
        var validationResult = await validator.ValidateAsync(sticker, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        await stickerRepository.CreateAsync(sticker, cancellationToken);
        
        return Unit.Value;
    }
}