using MediatR;
using TweetService.Application.DTOs.TweetsDto;

namespace TweetService.Application.UseCases.Queries.Tweet.GetTweets;

public record GetTweetsCommand : IRequest<IEnumerable<TweetResponseToDto>>;