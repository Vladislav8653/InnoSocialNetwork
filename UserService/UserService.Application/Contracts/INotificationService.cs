using UserService.Application.Producers.EmailProducer;

namespace UserService.Application.Contracts;

public interface INotificationService
{
    public Task SendEmailAsync(SendEmailEvent emailEvent);
}