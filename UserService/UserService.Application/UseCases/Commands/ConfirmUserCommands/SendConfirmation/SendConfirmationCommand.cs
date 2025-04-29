using MediatR;

namespace UserService.Application.UseCases.Commands.ConfirmUserCommands.SendConfirmation;

public record SendConfirmationCommand : IRequest<Unit>
{
    public string? UserId { get; init; }
}