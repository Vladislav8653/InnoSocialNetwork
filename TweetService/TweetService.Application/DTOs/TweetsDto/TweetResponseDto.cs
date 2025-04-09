namespace TweetService.Application.DTOs.TweetsDto;

public record TweetResponseDto
{
    public Guid Id { get; init; }
    public Guid WriterId { get; init; }
    public string Title { get; init; } 
    public string Content { get; init; } 
    public DateTime Created { get; init; }
    public DateTime Modified { get; init; } 
}