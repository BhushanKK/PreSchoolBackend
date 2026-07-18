using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Infrastructure.Services;

public class EmailTemplateService : IEmailTemplateService
{
    private const string EmailTemplatesFolder = "EmailTemplates";

    public async Task<string> GetForgotPasswordTemplateAsync(string userName, string resetLink)
    {
        var placeholders = new Dictionary<string, string>
        {
            { "{{UserName}}", userName },
            { "{{ResetLink}}", resetLink }
        };

        return await LoadTemplateAsync("ForgotPassword.html", placeholders);
    }

    private static async Task<string> LoadTemplateAsync(string templateName, Dictionary<string, string> placeholders)
    {
        var templatePath = Path.Combine(AppContext.BaseDirectory, EmailTemplatesFolder, templateName);

        if (!File.Exists(templatePath))
            throw new FileNotFoundException($"Email template '{templateName}' was not found.", templatePath);

        var html = await File.ReadAllTextAsync(templatePath);

        foreach (var placeholder in placeholders)
            html = html.Replace(placeholder.Key, placeholder.Value, StringComparison.OrdinalIgnoreCase);

        return html;
    }
}