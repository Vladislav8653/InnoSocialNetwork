namespace TweetService.Application.DTOs.TweetsDto;

public record TweetRequestToDto
{
    public string Title { get; init; } 
    public string Content { get; init; } 
}