using AutoMapper;
using MediatR;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.DTOs.TweetsDto;
using TweetService.Application.Pagination;

namespace TweetService.Application.UseCases.Queries.Tweet.GetTweets;

public class GetTweetsCommandHandler(
    ITweetRepository tweetRepository,
    IMapper mapper) : 
    IRequestHandler<GetTweetsCommand, PagedResult<TweetResponseDto>>
{
    public async Task<PagedResult<TweetResponseDto>> Handle(GetTweetsCommand request, CancellationToken cancellationToken)
    {
        var tweets = await tweetRepository.GetByPageAsync(
            request.PageParams, false, cancellationToken);
        
        var tweetsResponseDto = mapper.Map<IEnumerable<TweetResponseDto>>(tweets.Items);
        
        return new PagedResult<TweetResponseDto>(tweetsResponseDto, tweets.Total);
    }
}