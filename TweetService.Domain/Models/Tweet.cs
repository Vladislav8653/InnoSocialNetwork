namespace TweetService.Domain.Models;

public class Tweet
{
    public Guid Id { get; set; }
    public Guid WriterId { get; set; }
    public string Title { get; set; } 
    public string Content { get; set; } 
    public DateTime Created { get; set; }
    public DateTime Modified { get; set; } 
    public ICollection<Sticker> Stickers { get; set; } = new List<Sticker>();
}