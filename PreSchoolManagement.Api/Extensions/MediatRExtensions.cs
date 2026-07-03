using SchoolAdmission.Application.Features.Commands;

namespace SchoolAdmission.Api.Extensions;

public static class MediatRExtensions
{
    public static IServiceCollection AddMediatRServices(this IServiceCollection services)
    {
        services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateCasteMasterCommand).Assembly));
            services.AddMediatR(cfg =>
            cfg.RegisterServicesFromAssembly(typeof(CreateReligionMasterCommand).Assembly));
        return services;
    }
}
