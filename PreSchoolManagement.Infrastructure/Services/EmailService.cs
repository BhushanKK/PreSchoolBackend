using MimeKit;
using MailKit.Net.Smtp;
using MailKit.Security;
using Microsoft.Extensions.Options;
using SchoolManagement.Shared.Constants;

namespace PreSchoolManagement.Infrastructure.Interfaces;

public class EmailService(IOptions<EmailSettings> options) : IEmailService
{
    private readonly EmailSettings _settings = options.Value;

    public async Task SendAsync(
        string toEmail,
        string subject,
        string body,
        bool isHtml = true)
    {
        var email = new MimeMessage();

        email.From.Add(
            new MailboxAddress(
                _settings.DisplayName,
                _settings.FromEmail));

        email.To.Add(
            MailboxAddress.Parse(toEmail));

        email.Subject = subject;

        email.Body = new BodyBuilder
        {
            HtmlBody = isHtml ? body : null,
            TextBody = isHtml ? null : body
        }.ToMessageBody();

        using var smtp = new SmtpClient();

        await smtp.ConnectAsync(
            _settings.Host,
            _settings.Port,
            _settings.EnableSsl
                ? SecureSocketOptions.StartTls
                : SecureSocketOptions.None);

        await smtp.AuthenticateAsync(
            _settings.UserName,
            _settings.Password);

        await smtp.SendAsync(email);
        await smtp.DisconnectAsync(true);
    }
}