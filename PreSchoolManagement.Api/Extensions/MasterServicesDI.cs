using SchoolAdmission.Infrastructure.Interfaces;
using SchoolAdmission.Infrastructure.Services;

namespace SchoolAdmission.Api.Extensions;

public static class MasterServicesDI
{
    public static IServiceCollection AddMasterServices(this IServiceCollection services)
    {
        services.AddScoped<ICasteMasterService, CasteMasterService>();
        services.AddScoped<IAuthService, AuthService>();
        return services;
    }
}
