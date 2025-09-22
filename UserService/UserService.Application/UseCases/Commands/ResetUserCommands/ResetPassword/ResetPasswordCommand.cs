using MediatR;
using UserService.Application.DTO.PasswordResetDto;

namespace UserService.Application.UseCases.Commands.ResetUserCommands.ResetPassword;

public record ResetPasswordCommand : IRequest<Unit>
{
    public ResetPasswordDto ResetPasswordDto { get; init; }
}