using MediatR;

namespace UserService.Application.UseCases.Commands.UserCommands.DeleteById;

public record DeleteUserCommand : IRequest<Unit>
{ 
    public string UserId { get; init; } = null!;
}