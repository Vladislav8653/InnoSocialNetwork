using DiscussionService.Application.Commands;
using DiscussionService.Application.Contracts;
using DiscussionService.Domain.Models;
using MediatR;

namespace DiscussionService.Application.UseCases;

public class CreateMessageCommandHandler(IMessageRepository repository) : IRequestHandler<CreateMessageCommand, Unit>
{
    public async Task<Unit> Handle(CreateMessageCommand request, CancellationToken cancellationToken)
    {
        

       // await repository.CreateAsync(message);
        return Unit.Value;
    }
}
