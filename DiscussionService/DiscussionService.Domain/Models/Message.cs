using MongoDB.Bson;

namespace DiscussionService.Domain.Models;

public class Message
{
    public ObjectId Id { get; set; }
    public Guid TweetId { get; set; }
    public string Content { get; set; }
}