using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateDesignationMasterCommandValidator :AbstractValidator<CreateDesignationMasterCommand>
{
    public CreateDesignationMasterCommandValidator()
    {
        RuleFor(x => x.Designation)
            .NotEmpty().WithMessage("Designation  name is required.")
            .MaximumLength(100).WithMessage("Designation name must not be exceed 100 characters.");
       
    }
}
public class UpdateDesignationMasterCommandValidator : AbstractValidator<UpdateDesignationMasterCommand>
{
    public UpdateDesignationMasterCommandValidator()
    {
        RuleFor(x =>x.DesignationId)
            .GreaterThan(0).WithMessage("Designation Id is required.");

        RuleFor(x => x.Designation)
            .NotEmpty().WithMessage("Designation name is required.")
            .MaximumLength(100).WithMessage("Designaation name  must not exceed 100 character.");
    }
}