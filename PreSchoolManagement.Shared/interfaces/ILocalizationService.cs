
namespace PreSchoolManagement.Shared.Localization;

public interface ILocalizationService
{
    string Get(string key);
    string Get(string key, params object[] args);

    string Get(string resourceName, string key);
    string Get(string resourceName, string key, params object[] args);
}