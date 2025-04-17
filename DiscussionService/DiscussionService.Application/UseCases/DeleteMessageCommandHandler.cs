using DiscussionService.Application.Commands;
using DiscussionService.Application.Contracts;
using DiscussionService.Application.Exceptions;
using MediatR;

namespace DiscussionService.Application.UseCases;

public class DeleteMessageCommandHandler (
    IMessageRepository messageRepository) :
    IRequestHandler<DeleteMessageCommand, Unit>
{
    public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await messageRepository.GetByIdAsync(request.MessageId, cancellationToken);
        if (message is null)
            throw new NotFoundException($"Message with id {request.MessageId} not found");
        
        await messageRepository.DeleteAsync(request.MessageId, cancellationToken);
        
        return Unit.Value;
    }
}