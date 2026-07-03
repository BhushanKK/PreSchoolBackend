using FluentValidation;
using SchoolAdmission.Application.Features.Masters.Validators;

namespace SchoolAdmission.Api.Extensions;

public static class ValidatorExtensions
{
    public static IServiceCollection AddValidatorServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateCasteMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateReligionMasterCommandValidator>();
        return services;
    }
}
