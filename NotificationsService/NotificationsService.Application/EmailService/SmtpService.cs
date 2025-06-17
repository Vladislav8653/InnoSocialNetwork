using MailKit.Net.Smtp;
using Microsoft.Extensions.Options;
using MimeKit;
using NotificationsService.Application.Contracts.ServicesContracts;
using NotificationsService.Application.Settings;

namespace NotificationsService.Application.EmailService;

public class SmtpService(IOptions<SmtpServiceSettings> options) : ISmtpService
{
    private readonly SmtpServiceSettings _settings = options.Value;
    public async Task SendEmailAsync(string name, string toEmail, string subject, string body)
    {
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(_settings.SenderName, _settings.SenderEmail));
        message.To.Add(new MailboxAddress(name, toEmail));
        
        message.Subject = subject;
        var bodyBuilder = new BodyBuilder { HtmlBody = body };
        message.Body = bodyBuilder.ToMessageBody();

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(
            _settings.SmtpServer, 
            _settings.Port,
            MailKit.Security.SecureSocketOptions.StartTls);
        
        await smtp.AuthenticateAsync(_settings.SenderEmail, _settings.SenderPassword);
        await smtp.SendAsync(message);
        await smtp.DisconnectAsync(true);
    }
}
