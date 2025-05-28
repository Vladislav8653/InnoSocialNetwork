namespace NotificationsService.Application.DTOs.DigestDto;

public record TweetDigestItemDto
{
    public string TweetId { get; set; }
    public string Author { get; set; }
    public string Content { get; set; }
    public DateTime CreatedAt { get; set; } 
    public DateTime ModifiedAt { get; set; } 
}