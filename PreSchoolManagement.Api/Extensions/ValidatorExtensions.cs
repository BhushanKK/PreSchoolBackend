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
<<<<<<< HEAD
        services.AddValidatorsFromAssemblyContaining<CreateRoleMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateMenuMasterCommandValidator>();
        
=======
        services.AddValidatorsFromAssemblyContaining<CreateSectionMasterCommandValidator>();
>>>>>>> d22bc090a7cc203cdca844c8825eb0f498f4f8d6
        return services;
    }
}
