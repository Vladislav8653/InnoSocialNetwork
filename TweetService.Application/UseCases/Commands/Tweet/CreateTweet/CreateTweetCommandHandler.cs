using AutoMapper;
using FluentValidation;
using MediatR;
using TweetService.Application.Contracts.RepositoryContracts;

namespace TweetService.Application.UseCases.Commands.Tweet.CreateTweet;

public class CreateTweetCommandHandler(
    IMapper mapper, 
    ITweetRepository tweetRepository, 
    IValidator<Domain.Models.Tweet> validator) : 
    IRequestHandler<CreateTweetCommand, Unit>
{
    public async Task<Unit> Handle(CreateTweetCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UserId, out var userIdGuid))
        {
            throw new ValidationException("UserId is invalid");
        }
        
        var tweet = mapper.Map<Domain.Models.Tweet>(request.NewTweet);
        tweet.WriterId = userIdGuid;
        tweet.Created = DateTime.UtcNow;
        
        var validationResult = await validator.ValidateAsync(tweet, cancellationToken);
        if (!validationResult.IsValid)
            throw new ValidationException(validationResult.Errors);
        
        await tweetRepository.CreateAsync(tweet, cancellationToken);
        
        return Unit.Value;
    }
}