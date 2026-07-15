namespace PreSchoolManagement.Infrastructure.Interfaces;
public interface IEmailTemplateService
{
    Task<string> GetForgotPasswordTemplateAsync(
        string userName,
        string resetLink);

    Task<string> GetPasswordChangedTemplateAsync(
        string userName);
}