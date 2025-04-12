using DiscussionService.Application.Contracts;
using DiscussionService.Application.DTOs;
using DiscussionService.Application.Pagination;
using DiscussionService.Application.Queries;
using DiscussionService.Application.UseCases;
using DiscussionService.Domain.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;

namespace DiscussionService.Presentation;

[ApiController]
[Route("messages")]
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
    public async Task<IActionResult> GetById(ObjectId id)
    {
        var query = new GetMessageByIdQuery
        {
            Id = id
        };
        
        var response = await sender.Send(query);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] MessageRequestDto messageRequestDto)
    {
        throw new NotImplementedException();
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        throw new NotImplementedException();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Message message)
    {
        throw new NotImplementedException();
    }
}
