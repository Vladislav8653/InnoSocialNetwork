using FluentValidation;
using MediatR;
using TweetService.Application.Contracts.ProducerContracts;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.Exceptions;

namespace TweetService.Application.UseCases.Commands.Tweet.DeleteTweet;

public class DeleteTweetCommandHandler(
    ITweetRepository tweetRepository,
    ITweetDeletedProducer tweetDeletedProducer) :
    IRequestHandler<DeleteTweetCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTweetCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UserId, out var userIdGuid))
        {
            throw new ValidationException("UserId is invalid");
        }
        
        var tweets = await tweetRepository
            .FindByConditionAsync(tweet => tweet.Id == request.TweetId,
                false, cancellationToken);
        var tweet = tweets.FirstOrDefault();
        if (tweet is null)
            throw new NotFoundException($"Tweet with id {request.TweetId} not found");
        
        if (tweet.WriterId != userIdGuid)
        {
            throw new UnauthorizedAccessException("User is not authorized to delete this sticker");
        }
        
        await tweetRepository.DeleteAsync(tweet, cancellationToken);
        
        await tweetDeletedProducer.PublishTweetDeletedEvent(request.TweetId);
        
        return Unit.Value;
    }
}