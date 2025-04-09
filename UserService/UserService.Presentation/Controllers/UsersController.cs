using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using UserService.Application.DTO;
using UserService.Application.UseCases.Commands.UserCommands.Authenticate;
using UserService.Application.UseCases.Commands.UserCommands.DeleteById;
using UserService.Application.UseCases.Commands.UserCommands.Register;
using UserService.Application.UseCases.Queries.UserQueries.GetAllUsers;

namespace UserService.Presentation.Controllers;

[Route("api/users")]
[ApiController]
public class UsersController(IMediator mediator) : ControllerBase
{
    [HttpGet("getAllUsers")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> GetAllUsers()
    {
        var query = new GetAllUsersQuery();
        var users = await mediator.Send(query);
        
        return Ok(users);
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(
        [FromBody] UserRequestDto userRequestDto)
    {
        var query = new RegisterUserCommand
        {
            UserRequestDto = userRequestDto
        };
        await mediator.Send(query);
        
        return Ok();
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> Authenticate([FromBody] AuthenticateUserDto userForLogin)
    {
        var query = new AuthenticateUserCommand
        {
            AuthenticateUserDto = userForLogin
        };
        var (accessToken, refreshToken) = await mediator.Send(query);
         
        return Ok(new { AccessToken = accessToken, RefreshToken = refreshToken });
    }

    [HttpDelete("deleteUser/{userId}")]
    [Authorize(Policy = "Admin")]
    public async Task<IActionResult> DeleteUser(string userId)
    {
        var query = new DeleteUserCommand
        {
            UserId = userId
        };
        await mediator.Send(query);
        
        return Ok();
    }
}
