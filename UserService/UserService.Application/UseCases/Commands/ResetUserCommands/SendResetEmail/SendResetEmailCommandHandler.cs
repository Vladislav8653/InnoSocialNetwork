using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Contracts.SmtpContracts;
using UserService.Application.Exceptions;
using UserService.Domain.Models;

namespace UserService.Application.UseCases.Commands.ResetUserCommands.SendResetEmail;

public class SendResetEmailCommandHandler(
    UserManager<User> userManager,
    ISmtpService smtpService)
    : IRequestHandler<SendResetEmailCommand, string>
{
    public async Task<string> Handle(SendResetEmailCommand request, CancellationToken cancellationToken)
    {
        var user = await userManager.FindByEmailAsync(request.NewPasswordDto.Email);
        if (user == null)
        {
            throw new NotFoundException($"User with email {request.NewPasswordDto.Email} not found");
        }
        
        const string subject = "Reset Password";
        var body = $"Your reset token: {await userManager.GeneratePasswordResetTokenAsync(user)}";
        await smtpService.SendEmailAsync(
            user.UserName!, user.Email!, subject, body);
        return "Reset token sent successfully.";
    }
}