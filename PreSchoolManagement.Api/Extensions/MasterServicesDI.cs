using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Infrastructure.Services;

namespace PreSchoolManagement.Api.Extensions;

public static class MasterServicesDI
{
    public static IServiceCollection AddMasterServices(this IServiceCollection services)
    {
        services.AddScoped<ICasteMasterService, CasteMasterService>();
        services.AddScoped<IReligionMasterService, ReligionMasterService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAcademicYearMasterService, AcademicYearMasterService>();
        services.AddScoped<IFinancialYearMasterService, FinancialYearMasterService>();
        
        return services;
    }
}
