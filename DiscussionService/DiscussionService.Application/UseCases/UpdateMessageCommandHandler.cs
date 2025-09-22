using AutoMapper;
using DiscussionService.Application.Commands;
using DiscussionService.Application.Contracts;
using DiscussionService.Application.Exceptions;
using DiscussionService.Domain.Models;
using FluentValidation;
using MediatR;

namespace DiscussionService.Application.UseCases;

public class UpdateMessageCommandHandler(
    IMessageRepository messageRepository,
    IMapper mapper,
    IValidator<Message> validator) : 
    IRequestHandler<UpdateMessageCommand, Unit>
{
    public async Task<Unit> Handle(UpdateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await messageRepository.GetByIdAsync(request.MessageId, cancellationToken);
        if (message is null)
            throw new NotFoundException($"Message with id {request.MessageId} not found");
            
        mapper.Map(request.MessageDto, message);
        
        var validationResult = await validator.ValidateAsync(message, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        await messageRepository.UpdateAsync(message, request.MessageId, cancellationToken);
        
        return Unit.Value;
    }
}