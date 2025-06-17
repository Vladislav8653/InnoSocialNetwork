using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTO;
using UserService.Application.UseCases.Commands.TokenCommands.RefreshToken;

namespace UserService.Presentation.Controllers;

[Route("api/token")]
[ApiController]
public class TokenController(
    IMediator mediator) : ControllerBase
{
    [HttpPost("refresh")]
    public async Task<IActionResult> Refresh([FromBody] TokenDto tokenDto)
    {
        var command = new RefreshTokenCommand
        {
            TokenDto = tokenDto
        };
        var accessToken = await mediator.Send(command);
        
        return Ok(accessToken);
    }
}
