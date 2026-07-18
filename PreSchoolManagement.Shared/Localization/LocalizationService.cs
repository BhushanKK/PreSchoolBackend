using System.Collections.Concurrent;
using System.Globalization;
using System.Resources;

namespace PreSchoolManagement.Shared.Localization;
public class LocalizationService : ILocalizationService
{
    private const string ResourceNamespace = "PreSchoolManagement.Shared.Localization";
    private static readonly ConcurrentDictionary<string, ResourceManager> ResourceManagers = new();

    private static ResourceManager GetResourceManager(string resourceName)
    {
        return ResourceManagers.GetOrAdd(resourceName,
            name => new ResourceManager(
                $"{ResourceNamespace}.{name}",
                typeof(LocalizationService).Assembly));
    }

    // Default Resource (ApiMessageResponse.resx)
    public string Get(string key)
        => Get("ApiMessageResponse", key);

    public string Get(string key, params object[] args)
        => Get("ApiMessageResponse", key, args);

    // Specific Resource
    public string Get(string resourceName, string key)
        => GetResourceManager(resourceName)
            .GetString(key, CultureInfo.CurrentUICulture) ?? key;

    public string Get(string resourceName, string key, params object[] args)
    {
        var value = GetResourceManager(resourceName)
            .GetString(key, CultureInfo.CurrentUICulture) ?? key;

        return string.Format(value, args);
    }
}