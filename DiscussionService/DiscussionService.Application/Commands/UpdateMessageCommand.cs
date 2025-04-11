using DiscussionService.Application.DTOs;
using MediatR;
using MongoDB.Bson;

namespace DiscussionService.Application.Commands;

public record UpdateMessageCommand : IRequest<Unit>
{
    public ObjectId MessageId { get; init; }
    public MessageRequestDto MessageDto  { get; init; } 
}