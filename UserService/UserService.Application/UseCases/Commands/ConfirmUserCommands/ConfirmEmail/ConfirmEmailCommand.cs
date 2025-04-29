using MediatR;

namespace UserService.Application.UseCases.Commands.ConfirmUserCommands.ConfirmEmail;

public record ConfirmEmailCommand : IRequest<Unit>
{
    public string? UserId { get; init; }
    public string ConfirmationCode { get; init; } 
}