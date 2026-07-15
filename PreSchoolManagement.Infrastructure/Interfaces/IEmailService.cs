namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface IEmailService
{
    Task SendAsync(
        string toEmail,
        string subject,
        string body,
        bool isHtml = true);
}