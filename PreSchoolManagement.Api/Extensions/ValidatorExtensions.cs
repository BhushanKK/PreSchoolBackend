using FluentValidation;
using SchoolAdmission.Application.Features.Masters.CasteMaster.Validators;

namespace SchoolAdmission.Api.Extensions;

public static class ValidatorExtensions
{
    public static IServiceCollection AddValidatorServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateCasteMasterCommandValidator>();

        return services;
    }
}
