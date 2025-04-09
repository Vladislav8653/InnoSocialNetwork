namespace UserService.Application.Contracts.SmtpContracts;

public interface ISmtpService
{
    public Task SendEmailAsync(string name, string email, string subject, string body);
}