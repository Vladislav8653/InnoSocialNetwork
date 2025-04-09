namespace TweetService.Domain.Models;

public class Sticker
{
    public Guid Id { get; set; }
    public string Text { get; set; }
    public ICollection<Tweet> Tweets { get; set; } = new List<Tweet>();
}