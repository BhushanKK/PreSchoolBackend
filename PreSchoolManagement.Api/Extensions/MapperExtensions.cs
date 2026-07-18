using PreSchoolManagement.Application.Features.Mappings;

namespace PreSchoolManagement.Api.Extensions;

public static class MapperExtensions
{
    public static IServiceCollection AddMapperServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CasteMasterProfile));
        services.AddAutoMapper(typeof(CategoryMasterProfile));
        services.AddAutoMapper(typeof(ReligionMasterProfile));
        services.AddAutoMapper(typeof(AcademicYearMasterProfile));
        services.AddAutoMapper(typeof(FinancialYearMasterProfile));
        services.AddAutoMapper(typeof(RoleMasterProfile));
        services.AddAutoMapper(typeof(MenuMasterProfile));
        services.AddAutoMapper(typeof(SectionMasterProfile));
        services.AddAutoMapper(typeof(DivisionMasterProfile));
        services.AddAutoMapper(typeof(StandardMasterProfile));
        services.AddAutoMapper(typeof(HolidayMasterProfile));
        services.AddAutoMapper(typeof(DistrictMasterProfile));
        services.AddAutoMapper(typeof(StateMasterProfile));
        services.AddAutoMapper(typeof(EmployeeTypeMasterProfile));
        services.AddAutoMapper(typeof(DesignationMasterProfile));
        services.AddAutoMapper(typeof(BoardMasterProfile));
        services.AddAutoMapper(typeof(MediumMasterProfile));
        
        return services;
    }
}
