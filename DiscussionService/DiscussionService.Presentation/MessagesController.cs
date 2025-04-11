using DiscussionService.Application.Contracts;
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
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        throw new NotImplementedException();
    }
    
    [HttpGet]
    public async Task<IActionResult> GetById(ObjectId id)
    {
        var query = new GetMessageByTweetIdQuery
        {
            Id = id
        };
        
        var response = await sender.Send(query);
        
        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Message message)
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
