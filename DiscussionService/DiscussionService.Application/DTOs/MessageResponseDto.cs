using MongoDB.Bson;

namespace DiscussionService.Application.DTOs;

public record MessageResponseDto
{
    public ObjectId Id { get; init; }
    public Guid TweetId { get; init; }
    public string Content { get; init; }
}