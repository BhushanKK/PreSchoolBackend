using SchoolAdmission.Application.Features.Mappings;

namespace SchoolAdmission.Api.Extensions;

public static class MapperExtensions
{
    public static IServiceCollection AddMapperServices(this IServiceCollection services)
    {
        services.AddAutoMapper(typeof(CasteMasterProfile));

        return services;
    }
}
