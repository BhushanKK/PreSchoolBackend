using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateEmployeeTypeMasterCommandValidator
    : AbstractValidator<CreateEmployeeTypeMasterCommand>
{
    public CreateEmployeeTypeMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.EmployeeTypeName)
            .Required(localizer, "EmployeeTypeName")
            .MaxLengthLocalized(localizer, "EmployeeTypeName", 100);

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0)
            .WithMessage(
                localizer.Get(
                    "ValidationMessages",
                    "GreaterThanOrEqual",
                    localizer.Get("ValidationMessages", "DisplayOrder"),
                    "0"));
    }
}

public class UpdateEmployeeTypeMasterCommandValidator
    : AbstractValidator<UpdateEmployeeTypeMasterCommand>
{
    public UpdateEmployeeTypeMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.EmployeeTypeId)
            .RequiredId(localizer, "EmployeeTypeId");

        RuleFor(x => x.EmployeeTypeName)
            .Required(localizer, "EmployeeTypeName")
            .MaxLengthLocalized(localizer, "EmployeeTypeName", 100);

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0)
            .WithMessage(
                localizer.Get(
                    "ValidationMessages",
                    "GreaterThanOrEqual",
                    localizer.Get("ValidationMessages", "DisplayOrder"),
                    "0"));
    }
}