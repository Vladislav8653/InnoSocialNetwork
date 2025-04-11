using DiscussionService.Application.DTOs;
using MongoDB.Bson;

namespace DiscussionService.Application.Commands;

public record UpdateMessageCommand
{
    public ObjectId MessageId { get; init; }
    public MessageRequestDto MessageDto  { get; init; } 
}