using MediatR;

namespace TweetService.Application.UseCases.Commands.Tweet.DeleteTweet;

public class DeleteTweetCommandHandler : IRequestHandler<DeleteTweetCommand, Unit>
{
    public async Task<Unit> Handle(DeleteTweetCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}