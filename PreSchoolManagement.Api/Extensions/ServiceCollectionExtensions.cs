using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Infrastructure.Services;
using PreSchoolManagement.Shared.Common;
using PreSchoolManagement.Shared.Localization;
using SchoolManagement.Domain;

namespace PreSchoolManagement.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton<AuditContext>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        services.AddSingleton<ILocalizationService,LocalizationService>();
        services.AddScoped<IMessageHelper, MessageHelper>();
        services.AddScoped<ILanguageService, LanguageService>();
        return services;
    }
}
