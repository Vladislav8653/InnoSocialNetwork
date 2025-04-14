using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TweetService.Application.DTOs.TweetsDto;
using TweetService.Application.Pagination;
using TweetService.Application.UseCases.Commands.Tweet.CreateTweet;
using TweetService.Application.UseCases.Commands.Tweet.DeleteTweet;
using TweetService.Application.UseCases.Commands.Tweet.UpdateTweet;
using TweetService.Application.UseCases.Queries.Tweet.GetTweetById;
using TweetService.Application.UseCases.Queries.Tweet.GetTweets;

namespace TweetService.Presentation.Controllers;

[ApiController]
[Route("api/tweets")]
public class TweetsController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetTweets([FromQuery] PageParams pageParams, CancellationToken cancellationToken)
    {
        var query = new GetTweetsCommand
        {
            PageParams = pageParams,
        };
        
        var tweets = await sender.Send(query, cancellationToken);
        
        return Ok(tweets);
    }

    
    [HttpGet("{tweetId:guid}")]
    public async Task<IActionResult> GetTweetById(Guid tweetId, 
        CancellationToken cancellationToken)
    {
        var query = new GetTweetByIdCommand
        {
            Id = tweetId
        };
        
        var tweet = await sender.Send(query, cancellationToken);
        
        return Ok(tweet);
    }

    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateTweet([FromBody] TweetRequestDto request,
        CancellationToken cancellationToken)
    {
        var query = new CreateTweetCommand
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            NewTweet = request
        };
        
        await sender.Send(query, cancellationToken);
        
        return NoContent();
    }
    
    //[Authorize]
    [HttpPut("{tweetId:guid}")]
    public async Task<IActionResult> UpdateTweet(
        [FromBody] TweetRequestDto request,
        Guid tweetId,
        CancellationToken cancellationToken)
    {
        var query = new UpdateTweetCommand
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            NewTweet = request,
            TweetId = tweetId
        };
        
        await sender.Send(query, cancellationToken);
        
        return NoContent();
    }
    
    //[Authorize]
    [HttpDelete("{tweetId:guid}")]
    public async Task<IActionResult> DeleteTweet(Guid tweetId,
        CancellationToken cancellationToken)
    {
        var query = new DeleteTweetCommand
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            TweetId = tweetId
        };
        
        await sender.Send(query, cancellationToken);
        
        return NoContent();
    }
    
}