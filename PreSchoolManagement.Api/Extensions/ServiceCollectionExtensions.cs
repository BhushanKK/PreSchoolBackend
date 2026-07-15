using PreSchoolManagement.Infrastructure.Interfaces;
using SchoolManagement.Domain;

namespace PreSchoolManagement.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton<AuditContext>();
        services.AddHttpContextAccessor();
        services.AddScoped<ICurrentUserService, CurrentUserService>();
        
        return services;
    }
}
