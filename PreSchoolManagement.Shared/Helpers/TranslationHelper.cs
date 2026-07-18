using SchoolManagement.Shared.Constants;

namespace PreSchoolManagement.Shared.Common;

public static class TranslationHelper
{
    public static string GetTranslatedValue<TTranslation>(
        IEnumerable<TTranslation> translations,
        string language,
        Func<TTranslation, string> languageSelector,
        Func<TTranslation, string> valueSelector,
        string defaultValue)
    {
        if (language == Languages.English)
            return defaultValue;

        var translation = translations.FirstOrDefault(x => languageSelector(x) == language);

        return translation == null
            ? defaultValue
            : valueSelector(translation);
    }
}