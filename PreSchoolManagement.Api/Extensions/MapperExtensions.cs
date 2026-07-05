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

        return services;
    }
}
