using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateEmployeeTypeMasterCommandValidator :AbstractValidator<CreateEmployeeTypeMasterCommand>
{
    public CreateEmployeeTypeMasterCommandValidator()
    {
        RuleFor(x => x.EmployeeTypeName)
            .NotEmpty().WithMessage("Employee Type name is required.")
            .MaximumLength(100).WithMessage("Employee Type name must not exceed 100 characters.");

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0).WithMessage("Display Order must be greater than or equal to 0.");
    }
}

public class UpdateEmployeeTypeMasterCommandValidator : AbstractValidator<UpdateEmployeeTypeMasterCommand>
{
    public UpdateEmployeeTypeMasterCommandValidator()
    {
        RuleFor(x => x.EmployeeTypeId)
            .GreaterThan(0).WithMessage("Employee Type Id is required.");

        RuleFor(x => x.EmployeeTypeName)
            .NotEmpty().WithMessage("Employee Type name is required.")
            .MaximumLength(100).WithMessage("Employee Type name must not exceed 100 characters.");

        RuleFor(x => x.DisplayOrder)
            .GreaterThanOrEqualTo(0).WithMessage("Display Order must be greater than or equal to 0.");
    }
}