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
        var products = await tweetRepository.GetByPageAsync(
            request.PageParams, false, cancellationToken);
        
        var productsResponseDto = mapper.Map<IEnumerable<TweetResponseToDto>>(products.Items);
        
        return new PagedResult<TweetResponseToDto>(productsResponseDto, products.Total);
    }
}