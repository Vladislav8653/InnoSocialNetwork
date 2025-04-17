using DiscussionService.Application.Commands;
using DiscussionService.Application.DTOs;
using DiscussionService.Application.Pagination;
using DiscussionService.Application.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DiscussionService.Presentation;

[ApiController]
[Route("api/messages")]
public class MessagesController(ISender sender) : ControllerBase
{
    [HttpGet("{tweetId:guid}")]
    public async Task<IActionResult> GetAll([FromQuery] PageParams pageParams, Guid tweetId, 
        CancellationToken cancellationToken)
    {
        var query = new GetAllMessagesQuery
        {
            PageParams = pageParams,
            TweetId = tweetId
        };
        
        var messages = await sender.Send(query, cancellationToken);
        
        return Ok(messages);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(string id, CancellationToken cancellationToken)
    {
        var query = new GetMessageByIdQuery
        {
            Id = id
        };
        
        var response = await sender.Send(query, cancellationToken);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MessageRequestDto messageRequestDto, 
        CancellationToken cancellationToken)
    {
        var query = new CreateMessageCommand
        {
            MessageDto = messageRequestDto
        };
        
        var response = await sender.Send(query, cancellationToken);
        
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(ObjectId id, CancellationToken cancellationToken)
    {
        var query = new DeleteMessageCommand
        {
            MessageId = id
        };
        
        var response = await sender.Send(query, cancellationToken);
        
        return NoContent();
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Update(ObjectId id, [FromBody] MessageRequestDto message,
        CancellationToken cancellationToken)
    {
        var query = new UpdateMessageCommand
        {
            MessageId = id,
            MessageDto = message
        };
        
        var response = await sender.Send(query, cancellationToken);
        
        return NoContent();
    }
}
