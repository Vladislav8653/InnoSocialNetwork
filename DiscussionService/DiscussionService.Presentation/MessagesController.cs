using DiscussionService.Application.Contracts;
using DiscussionService.Domain.Models;
using Microsoft.AspNetCore.Mvc;

namespace DiscussionService.Presentation;

[ApiController]
[Route("messages")]
public class MessagesController : ControllerBase
{
    private readonly IMessageRepository _repository;

    public MessagesController(IMessageRepository repository)
    {
        _repository = repository;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var messages = await _repository.GetAllAsync();
        return Ok(messages);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] Message message)
    {
        await _repository.CreateAsync(message);
        return CreatedAtAction(nameof(GetAll), null);
    }

    [HttpDelete("{id:guid}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        await _repository.DeleteAsync(id);
        return NoContent();
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, [FromBody] Message message)
    {
        await _repository.UpdateAsync(message, id);
        return NoContent();
    }
}
