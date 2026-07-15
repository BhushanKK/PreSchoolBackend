using FluentValidation;
using PreSchoolManagement.Application.Features.Auth.Validators;
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
        services.AddValidatorsFromAssemblyContaining<CreateRoleMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateMenuMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateSectionMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateDivisionMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateStandardMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateHolidayMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateDistrictMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateStateMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateEmployeeTypeMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<ChangePasswordCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateDesignationMasterCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<ChangePasswordCommandValidator>();
        services.AddValidatorsFromAssemblyContaining<ForgotPasswordValidator>();
        services.AddValidatorsFromAssemblyContaining<CreateCommitteeMasterCommandValidator>();
        return services;
    }
}
