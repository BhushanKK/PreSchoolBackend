using SchoolAdmission.Application.Features.CasteMasters.Commands;

namespace SchoolAdmission.Api.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateCasteMasterCommand).Assembly));

        return services;
    }
}
