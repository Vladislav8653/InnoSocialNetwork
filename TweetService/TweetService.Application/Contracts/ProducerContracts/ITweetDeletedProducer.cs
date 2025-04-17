namespace TweetService.Application.Contracts.ProducerContracts;

public interface ITweetDeletedProducer
{
    public Task PublishTweetDeletedEvent(Guid tweetId);
}