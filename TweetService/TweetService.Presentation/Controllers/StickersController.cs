using System.Security.Claims;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using TweetService.Application.DTOs.StickersDto;
using TweetService.Application.Pagination;
using TweetService.Application.UseCases.Commands.Sticker.CreateSticker;
using TweetService.Application.UseCases.Commands.Sticker.DeleteSticker;
using TweetService.Application.UseCases.Commands.Sticker.UpdateSticker;
using TweetService.Application.UseCases.Queries.Sticker.GetStickerById;
using TweetService.Application.UseCases.Queries.Sticker.GetStickers;

namespace TweetService.Presentation.Controllers;


[ApiController]
[Route("api/stickers")]
public class StickersController(ISender sender) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetStickers([FromQuery] PageParams pageParams, CancellationToken cancellationToken)
    {
        var query = new GetStickersCommand
        {
            PageParams = pageParams,
        };
        
        var stickers = await sender.Send(query, cancellationToken);
        
        return Ok(stickers);
    }

    
    [HttpGet("{stickerId:guid}")]
    public async Task<IActionResult> GetStickerById(Guid stickerId, 
        CancellationToken cancellationToken)
    {
        var query = new GetStickerByIdCommand
        {
            Id = stickerId
        };
        
        var sticker = await sender.Send(query, cancellationToken);
        
        return Ok(sticker);
    }

    //[Authorize]
    [HttpPost]
    public async Task<IActionResult> CreateSticker([FromBody] StickerRequestDto request,
        CancellationToken cancellationToken)
    {
        var query = new CreateStickerCommand
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            NewSticker = request
        };
        
        await sender.Send(query, cancellationToken);
        
        return NoContent();
    }
    
    //[Authorize]
    [HttpPut("{stickerId:guid}")]
    public async Task<IActionResult> UpdateSticker(
        [FromBody] StickerRequestDto request,
        Guid stickerId,
        CancellationToken cancellationToken)
    {
        var query = new UpdateStickerCommand
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            NewSticker = request,
            StickerId = stickerId
        };
        
        await sender.Send(query, cancellationToken);
        
        return NoContent();
    }
    
    //[Authorize]
    [HttpDelete("{stickerId:guid}")]
    public async Task<IActionResult> DeleteSticker(Guid stickerId,
        CancellationToken cancellationToken)
    {
        var query = new DeleteStickerCommand
        {
            UserId = User.FindFirstValue(ClaimTypes.NameIdentifier),
            StickerId = stickerId
        };
        
        await sender.Send(query, cancellationToken);
        
        return NoContent();
    }
    
}