using MediatR;
using TweetService.Application.DTOs.TweetsDto;

namespace TweetService.Application.UseCases.Queries.Tweet.GetTweets;

public class GetTweetsCommandHandler : IRequestHandler<GetTweetsCommand, IEnumerable<TweetResponseToDto>>
{
    public async Task<IEnumerable<TweetResponseToDto>> Handle(GetTweetsCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}