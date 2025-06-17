using AutoMapper;
using MediatR;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.DTOs.TweetsDto;
using TweetService.Application.Exceptions;

namespace TweetService.Application.UseCases.Queries.Tweet.GetTweetById;

public class GetTweetByIdCommandHandler(
    ITweetRepository tweetRepository,
    IMapper mapper) :
    IRequestHandler<GetTweetByIdCommand, TweetResponseDto>
{
    public async Task<TweetResponseDto> Handle(GetTweetByIdCommand request, CancellationToken cancellationToken)
    {
        var tweets = await tweetRepository.FindByConditionAsync(
            tweet => tweet.Id == request.Id,false, cancellationToken);
        var tweet = tweets.FirstOrDefault();
        if (tweet is null)
            throw new NotFoundException($"Tweet with id {request.Id} not found");
        
        var tweetResponseDto = mapper.Map<TweetResponseDto>(tweet);
        
        return tweetResponseDto;
    }
}