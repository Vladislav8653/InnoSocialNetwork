using AutoMapper;
using MediatR;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.DTOs.TweetsDto;

namespace TweetService.Application.UseCases.Queries.Tweet.GetTweetById;

public class GetTweetByIdCommandHandler(
    ITweetRepository tweetRepository,
    IMapper mapper) :
    IRequestHandler<GetTweetByIdCommand, TweetResponseToDto>
{
    public async Task<TweetResponseToDto> Handle(GetTweetByIdCommand request, CancellationToken cancellationToken)
    {
        var products = await tweetRepository.FindByConditionAsync(
            product => product.Id == request.Id,false, cancellationToken);
        var product = products.FirstOrDefault();
        if (product is null)
            throw new InvalidOperationException($"Tweet with id {request.Id} not found");
        
        var productResponseDto = mapper.Map<TweetResponseToDto>(product);
        
        return productResponseDto;
    }
}