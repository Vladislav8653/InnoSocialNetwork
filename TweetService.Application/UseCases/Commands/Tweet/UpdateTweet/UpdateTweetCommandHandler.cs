using AutoMapper;
using FluentValidation;
using MediatR;
using TweetService.Application.Contracts.RepositoryContracts;

namespace TweetService.Application.UseCases.Commands.Tweet.UpdateTweet;

public class UpdateTweetCommandHandler(
    ITweetRepository tweetRepository, 
    IMapper mapper,
    IValidator<Domain.Models.Tweet> validator) : 
    IRequestHandler<UpdateTweetCommand, Unit>
{
    public async Task<Unit> Handle(UpdateTweetCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UserId, out var userIdGuid))
        {
            throw new ValidationException("UserId is invalid");
        }
        
        var tweets = await tweetRepository.FindByConditionAsync(tweet => 
            tweet.Id == request.TweetId,false, cancellationToken);
        var tweet = tweets.FirstOrDefault();
        if (tweet is null)
            throw new InvalidOperationException($"Tweet with id {request.TweetId} not found");
        
        if (tweet.WriterId != userIdGuid)
        {
            throw new UnauthorizedAccessException("User is not authorized to update this product");
        }
            
        mapper.Map(request.NewTweet, tweet);
        
        var validationResult = await validator.ValidateAsync(tweet, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        await tweetRepository.UpdateAsync(tweet, cancellationToken);
        
        return Unit.Value;
    }
}