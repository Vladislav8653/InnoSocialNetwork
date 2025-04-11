namespace DiscussionService.Domain.Models;

public class Message
{
    public Guid Id { get; set; }
    public Guid TweetId { get; set; }
    public string Content { get; set; }
}