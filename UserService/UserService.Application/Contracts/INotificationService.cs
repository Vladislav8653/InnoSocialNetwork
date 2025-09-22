using UserService.Application.DTO;

namespace UserService.Application.Contracts;

public interface INotificationService
{
    public Task SendEmailAsync(EmailDto emailEvent);
}