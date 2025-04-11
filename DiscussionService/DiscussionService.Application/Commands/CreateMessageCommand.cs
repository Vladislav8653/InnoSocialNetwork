using MediatR;

namespace DiscussionService.Application.Commands;

public record CreateMessageCommand : IRequest<Unit>
{
   
}
