using NotificationsService.Application.Contracts;
using NotificationsService.Application.Contracts.ServicesContracts;
using NotificationsService.Application.EmailService;

namespace NotificationsService.Application.UseCases.KafkaHandlers;

public class SendEmailHandler(ISmtpService smtpService) : IEventHandler<SendEmailEvent>
{
    public async Task HandleAsync(SendEmailEvent message, CancellationToken cancellationToken)
    {
        await smtpService.SendEmailAsync(
            message.ToName,
            message.ToEmail,
            message.Subject,
            message.Body);
    }
}