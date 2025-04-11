using MongoDB.Bson;

namespace DiscussionService.Application.Commands;

public record DeleteMessageCommand
{
    public ObjectId MessageId { get; init; }
}