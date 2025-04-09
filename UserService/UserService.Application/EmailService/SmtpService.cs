using MailKit.Net.Smtp;
using Microsoft.Extensions.Configuration;
using MimeKit;
using UserService.Application.Contracts.SmtpContracts;

namespace UserService.Application.EmailService;

public class SmtpService(IConfiguration configuration) : ISmtpService
{
    public async Task SendEmailAsync(string name, string email, string subject, string body)
    {
        var emailConfiguration = configuration.GetSection("EmailConfiguration");
        
        var message = new MimeMessage();
        message.From.Add(new MailboxAddress(emailConfiguration["SenderName"], 
            emailConfiguration["SenderEmail"]));
        message.To.Add(new MailboxAddress(name, email));
        message.Subject = subject;
        message.Body = new TextPart("plain")
        {
            Text = body
        };
        
        var senderEmail = emailConfiguration["SenderEmail"];
        var senderPassword = emailConfiguration["Password"];
        using (var client = new SmtpClient())
        {
            await client.ConnectAsync(emailConfiguration["SmtpServer"], 
                int.Parse(emailConfiguration["Port"]!), 
                MailKit.Security.SecureSocketOptions.SslOnConnect);
            await client.AuthenticateAsync(senderEmail, senderPassword);
            await client.SendAsync(message);
            await client.DisconnectAsync(true);
        }

    }
}