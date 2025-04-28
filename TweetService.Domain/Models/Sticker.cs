namespace TweetService.Domain.Models;

public class Sticker
{
    public Guid Id { get; set; }
    public string Name { get; set; }
    public Guid UserId { get; set; }
    public ICollection<Tweet> Tweets { get; set; } = new List<Tweet>();
}