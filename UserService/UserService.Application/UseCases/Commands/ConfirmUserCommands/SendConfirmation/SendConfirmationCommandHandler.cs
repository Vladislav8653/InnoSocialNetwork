using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Identity;
using UserService.Application.Exceptions;
using UserService.Application.Producers.EmailProducer;
using UserService.Domain.Models;

namespace UserService.Application.UseCases.Commands.ConfirmUserCommands.SendConfirmation;

public class SendConfirmationCommandHandler(
    KafkaEmailProducer kafkaEmailProducer,
    UserManager<User> userManager) : 
    IRequestHandler<SendConfirmationCommand, Unit>
{
    public async Task<Unit> Handle(SendConfirmationCommand request, CancellationToken cancellationToken)
    {
        if (!Guid.TryParse(request.UserId, out var userIdGuid))
        {
            throw new ValidationException("UserId is invalid");
        }

        var user = await userManager.FindByIdAsync(userIdGuid.ToString());
        if (user == null)
        {
            throw new NotFoundException($"User with id {request.UserId} not found");
        }

        if (user.EmailConfirmed)
        {
            throw new NotFoundException($"User with email {user.Email} already confirmed");
        }
        
        var emailBody = $"Your confirmation code: {await userManager.GenerateEmailConfirmationTokenAsync(user)}";
        
        await kafkaEmailProducer.SendEmailAsync(new SendEmailEvent
        {
            ToName = user.UserName!, 
            ToEmail = user.Email!, 
            Subject = "Confirm your email", 
            Body =  emailBody
        });
        
        return Unit.Value;
    }
}