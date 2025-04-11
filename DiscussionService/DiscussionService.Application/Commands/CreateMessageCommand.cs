using DiscussionService.Application.DTOs;
using MediatR;

namespace DiscussionService.Application.Commands;

public record CreateMessageCommand : IRequest<Unit>
{
    public MessageRequestDto MessageDto  { get; init; } 
}
