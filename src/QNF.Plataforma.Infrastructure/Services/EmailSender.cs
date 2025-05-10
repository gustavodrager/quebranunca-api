using MailKit.Net.Smtp;
using MimeKit;
using QNF.Plataforma.Application.Interfaces;
using QNF.Plataforma.Application.Configurations;
using Microsoft.Extensions.Options;

namespace QNF.Plataforma.Infrastructure.Services;

public class EmailSender : IEmailSender
{
    private readonly EmailConfiguration _settings;

    public EmailSender(IOptions<EmailConfiguration> settings)
    {
        _settings = settings.Value;
    }

    public async Task SendEmailAsync(string to, string subject, string body)
    {
        var email = new MimeMessage();
        email.From.Add(MailboxAddress.Parse(_settings.Username));
        email.To.Add(MailboxAddress.Parse(to));
        email.Subject = subject;
        email.Body = new TextPart(MimeKit.Text.TextFormat.Text) { Text = body };

        using var smtp = new SmtpClient();
        await smtp.ConnectAsync(_settings.SmtpServer, _settings.SmtpPort, MailKit.Security.SecureSocketOptions.StartTls);
        await smtp.AuthenticateAsync(_settings.Username, _settings.Password);
        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}
