namespace NotificationsService.Application.Contracts.ServicesContracts;

public interface ISmtpService
{
    public Task SendEmailAsync(string name, string toEmail, string subject, string body);
}