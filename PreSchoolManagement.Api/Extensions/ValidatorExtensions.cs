using FluentValidation;
using PreSchoolManagement.Application.Features.Masters.Validators;

namespace PreSchoolManagement.Api.Extensions;

public static class ValidatorExtensions
{
    public static IServiceCollection AddValidatorServices(this IServiceCollection services)
    {
        services.AddValidatorsFromAssemblyContaining<CreateCategoryMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateCasteMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateReligionMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateAcademicYearMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateFinancialYearMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateSectionMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateDivisionMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateStandardMasterCommandValidator>();
        return services;
    }
}
