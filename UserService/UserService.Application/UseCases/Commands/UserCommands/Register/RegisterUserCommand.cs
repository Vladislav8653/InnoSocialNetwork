using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.DTO;

namespace UserService.Application.UseCases.Commands.UserCommands.Register;

public record RegisterUserCommand : IRequest<IdentityResult>
{
    public UserRequestDto UserRequestDto { get; init; } = null!;
}