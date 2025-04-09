using MediatR;
using UserService.Application.DTO.PasswordResetDto;
using UserService.Application.DTO;

namespace UserService.Application.UseCases.Commands.ResetUserCommands.SendResetEmail;

public record SendResetEmailCommand : IRequest<string>
{ 
    public EmailForPasswordResetDto NewPasswordDto { get; init; }
}