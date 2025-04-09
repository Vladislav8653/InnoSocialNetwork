using MediatR;
using UserService.Application.DTO.PasswordResetDto;
using UserService.Application.DTO;

namespace UserService.Application.UseCases.Commands.ResetUserCommands.ResetPassword;

public record ResetPasswordCommand : IRequest<string>
{
    public ResetPasswordDto ResetPasswordDto { get; init; }
}