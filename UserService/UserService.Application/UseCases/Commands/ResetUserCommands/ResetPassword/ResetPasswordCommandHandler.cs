using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Domain.Models;

namespace UserService.Application.UseCases.Commands.ResetUserCommands.ResetPassword;

public class ResetPasswordCommandHandler(
    UserManager<User> userManager)
    : IRequestHandler<ResetPasswordCommand, string>
{
    public async Task<string> Handle(ResetPasswordCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.ResetPasswordDto.Email);
        if (user == null)
        {
            throw new InvalidOperationException($"User with email {request.ResetPasswordDto.Email} not found");
        }

        var result = await userManager.ResetPasswordAsync(user,
            request.ResetPasswordDto.ResetToken, request.ResetPasswordDto.NewPassword);
        if (!result.Succeeded)
        {
            throw new InvalidOperationException("Invalid token.");
        }
        
        return "Password reset successfully.";
    }
}