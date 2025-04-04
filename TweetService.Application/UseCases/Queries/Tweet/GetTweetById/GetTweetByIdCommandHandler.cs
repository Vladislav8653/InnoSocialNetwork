using MediatR;
using TweetService.Application.DTOs.TweetsDto;

namespace TweetService.Application.UseCases.Queries.Tweet.GetTweetById;

public class GetTweetByIdCommandHandler : IRequestHandler<GetTweetByIdCommand, TweetResponseToDto>
{
    public async Task<TweetResponseToDto> Handle(GetTweetByIdCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}