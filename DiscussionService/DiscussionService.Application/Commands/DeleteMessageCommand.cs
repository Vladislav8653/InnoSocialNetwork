using MediatR;
using MongoDB.Bson;

namespace DiscussionService.Application.Commands;

public record DeleteMessageCommand : IRequest<Unit>
{
    public ObjectId MessageId { get; init; }
}