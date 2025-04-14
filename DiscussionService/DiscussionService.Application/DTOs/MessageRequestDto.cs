namespace DiscussionService.Application.DTOs;

public record MessageRequestDto
{
    public Guid TweetId { get; init; }
    public string Content { get; init; }
}