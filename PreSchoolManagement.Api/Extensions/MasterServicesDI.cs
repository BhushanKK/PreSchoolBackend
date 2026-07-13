using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Infrastructure.Services;

namespace PreSchoolManagement.Api.Extensions;

public static class MasterServicesDI
{
    public static IServiceCollection AddMasterServices(this IServiceCollection services)
    {
        services.AddScoped<ICasteMasterService, CasteMasterService>();
        services.AddScoped<ICategoryMasterService, CategoryMasterService>();
        services.AddScoped<IReligionMasterService, ReligionMasterService>();
        services.AddScoped<IAuthService, AuthService>();
        services.AddScoped<IAcademicYearMasterService, AcademicYearMasterService>();
        services.AddScoped<IFinancialYearMasterService, FinancialYearMasterService>();
        services.AddScoped<IRoleMasterService, RoleMasterService>();
        services.AddScoped<IMenuMasterService, MenuMasterService>();
        services.AddScoped<ISectionMasterService, SectionMasterService>();
        services.AddScoped<IDivisionMasterService, DivisionMasterService>();
        services.AddScoped<IStandardMasterService, StandardMasterService>();
        services.AddScoped<IHolidayMasterService, HolidayMasterService>();

        return services;
    }
}
