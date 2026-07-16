using PreSchoolManagement.Infrastructure.Interfaces;
using PreSchoolManagement.Infrastructure.Services;
using SchoolManagement.Shared.Constants;

namespace PreSchoolManagement.Api.Extensions;

public static class MasterServicesDI
{
    public static IServiceCollection AddMasterServices(this IServiceCollection services,IConfiguration configuration)
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
        services.AddScoped<IRoleMenuPermissionService, RoleMenuPermissionService>();
        services.AddScoped<IPermissionService, PermissionService>();
        services.AddScoped<IDistrictMasterService, DistrictMasterServices>();
        services.AddScoped<IAccountLockoutService, AccountLockoutService>();
        services.AddScoped<IStateMasterService, StateMasterService>();
        services.AddScoped<IEmployeeTypeMasterService, EmployeeTypeMasterService>();
        services.AddScoped<IDesignationMasterService, DesignationMasterService>();
        services.AddScoped<IPasswordResetService, PasswordResetService>();
        services.AddScoped<IEmailService, EmailService>();
        services.AddScoped<IEmailTemplateService, EmailTemplateService>();
        services.Configure<EmailSettings>(configuration.GetSection("EmailSettings"));
        services.Configure<FrontendSettings>(configuration.GetSection("FrontendSettings"));
        services.AddScoped<ICommitteeMasterService, CommitteeMasterService>();
        services.AddScoped<IBoardMasterService, BoardMasterService>();

        services.AddScoped<IFileStorageService, FileStorageService>();

        
        return services;
    }
}
