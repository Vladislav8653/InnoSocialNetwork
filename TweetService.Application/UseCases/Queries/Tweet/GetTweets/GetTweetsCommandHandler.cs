using AutoMapper;
using MediatR;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.DTOs.TweetsDto;
using TweetService.Application.Pagination;

namespace TweetService.Application.UseCases.Queries.Tweet.GetTweets;

public class GetTweetsCommandHandler(
    ITweetRepository tweetRepository,
    IMapper mapper) : 
    IRequestHandler<GetTweetsCommand, PagedResult<TweetResponseToDto>>
{
    public async Task<PagedResult<TweetResponseToDto>> Handle(GetTweetsCommand request, CancellationToken cancellationToken)
    {
        var tweets = await tweetRepository.GetByPageAsync(
            request.PageParams, false, cancellationToken);
        
        var tweetsResponseDto = mapper.Map<IEnumerable<TweetResponseToDto>>(tweets.Items);
        
        return new PagedResult<TweetResponseToDto>(tweetsResponseDto, tweets.Total);
    }
}