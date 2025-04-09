namespace TweetService.Application.DTOs.TweetsDto;

public record TweetRequestDto
{
    public string Title { get; init; } 
    public string Content { get; init; } 
}