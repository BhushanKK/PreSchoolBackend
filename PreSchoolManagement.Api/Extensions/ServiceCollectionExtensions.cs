using SchoolManagement.Domain;

namespace SchoolAdmission.Api.Extensions;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddCommonServices(this IServiceCollection services)
    {
        services.AddSingleton<AuditContext>();
        return services;
    }
}
