using MediatR;

namespace UserService.Application.UseCases.Commands.ConfirmUserCommands.SendConfirmation;

public record SendConfirmationCommand : IRequest<string>
{
    public string? UserId { get; init; }
}