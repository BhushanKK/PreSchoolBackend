using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Api.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCategoryMasterCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateCasteMasterCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateReligionMasterCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateAcademicYearMasterCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateFinancialYearMasterCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateRoleMasterCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateMenuMasterCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateRoleMasterCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateSectionMasterCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateDivisionMasterCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateStandardMasterCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateHolidayMasterCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateDistrictMasterCommand).Assembly));
        services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(CreateStateMasterCommand).Assembly));
        
        return services;
    }
}
