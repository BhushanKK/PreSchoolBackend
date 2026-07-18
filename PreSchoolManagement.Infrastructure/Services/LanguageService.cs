using System.Globalization;
using PreSchoolManagement.Infrastructure.Interfaces;

namespace PreSchoolManagement.Infrastructure.Services;

public class LanguageService : ILanguageService
{
    public string CurrentLanguage
    {
        get
        {
            var language = CultureInfo.CurrentUICulture.TwoLetterISOLanguageName;
            return string.IsNullOrWhiteSpace(language) ? "en"
                : language.ToLowerInvariant();
        }
    }
}