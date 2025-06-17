using MediatR;
using UserService.Application.DTO;

namespace UserService.Application.UseCases.Commands.UserCommands.Authenticate;

public record AuthenticateUserCommand
    : IRequest<(string AccessToken, string RefreshToken)>
{
    public AuthenticateUserDto AuthenticateUserDto { get; init; } = null!;
}