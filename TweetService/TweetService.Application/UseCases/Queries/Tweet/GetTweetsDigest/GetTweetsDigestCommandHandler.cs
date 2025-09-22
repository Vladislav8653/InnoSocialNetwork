using AutoMapper;
using MediatR;
using TweetService.Application.Contracts.RepositoryContracts;
using TweetService.Application.DTOs.TweetsDto;

namespace TweetService.Application.UseCases.Queries.Tweet.GetTweetsDigest;

public class GetTweetsDigestCommandHandler(
    ITweetRepository tweetRepository,
    IMapper mapper) : 
    IRequestHandler<GetTweetsDigestCommand, IEnumerable<TweetDigestDto>>
{
    public async Task<IEnumerable<TweetDigestDto>> Handle(GetTweetsDigestCommand request, CancellationToken cancellationToken)
    {
        var tweets = await tweetRepository.FindByConditionAsync(
            t => t.Created < request.EndDate && t.Created > request.StartDate,false, cancellationToken);
        
        var tweetsDigest = mapper.Map<IEnumerable<TweetDigestDto>>(tweets);

        return tweetsDigest;
    }
}