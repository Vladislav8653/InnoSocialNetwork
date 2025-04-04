using MediatR;

namespace TweetService.Application.UseCases.Commands.Tweet.CreateTweet;

public class CreateTweetCommandHandler : IRequestHandler<CreateTweetCommand, Unit>
{
    public async Task<Unit> Handle(CreateTweetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}