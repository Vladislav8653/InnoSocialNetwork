using DiscussionService.Application.Commands;
using DiscussionService.Application.Contracts;
using MediatR;

namespace DiscussionService.Application.UseCases;

public class DeleteMessageCommandHandler (
    IMessageRepository messageRepository) :
    IRequestHandler<DeleteMessageCommand, Unit>
{
    public async Task<Unit> Handle(DeleteMessageCommand request, CancellationToken cancellationToken)
    {
        var message = await messageRepository.GetByIdAsync(request.MessageId);
        if (message is null)
            throw new InvalidOperationException($"Message with id {request.MessageId} not found");
        
        await messageRepository.DeleteAsync(request.MessageId);
        
        return Unit.Value;
    }
}