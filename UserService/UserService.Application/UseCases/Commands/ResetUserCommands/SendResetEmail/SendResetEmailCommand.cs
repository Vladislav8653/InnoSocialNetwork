using MediatR;
using UserService.Application.DTO.PasswordResetDto;

namespace UserService.Application.UseCases.Commands.ResetUserCommands.SendResetEmail;

public record SendResetEmailCommand : IRequest<Unit>
{ 
    public EmailForPasswordResetDto NewPasswordDto { get; init; }
}