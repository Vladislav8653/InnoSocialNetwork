using AutoMapper;
using DiscussionService.Application.Commands;
using DiscussionService.Application.Contracts;
using DiscussionService.Domain.Models;
using FluentValidation;
using MediatR;

namespace DiscussionService.Application.UseCases;

public class CreateMessageCommandHandler(
    IMessageRepository messageRepository,
    IMapper mapper,
    IValidator<Message> validator) : IRequestHandler<CreateMessageCommand, Unit>
{
    public async Task<Unit> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        var message = mapper.Map<Message>(request.MessageDto);
        
        var validationResult = await validator.ValidateAsync(message, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        await messageRepository.CreateAsync(message, cancellationToken);
        
        return Unit.Value;
    }
}
