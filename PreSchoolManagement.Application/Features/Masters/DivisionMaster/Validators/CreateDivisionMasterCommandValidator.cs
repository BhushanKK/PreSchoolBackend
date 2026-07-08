using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateDivisionMasterCommandValidator : AbstractValidator<CreateDivisionMasterCommand>
{
    public CreateDivisionMasterCommandValidator()
    {
        RuleFor(x => x.DivisionName)
            .NotEmpty().WithMessage("Division name is required.")
            .MaximumLength(100).WithMessage("Division name must not exceed 100 characters.");
    }
}

public class UpdateDivisionMasterCommandValidator : AbstractValidator<UpdateDivisionMasterCommand>
{
    public UpdateDivisionMasterCommandValidator()
    {
        RuleFor(x => x.DivisionId)
            .GreaterThan(0).WithMessage("DivisionID is required.");

        RuleFor(x => x.DivisionName)
            .NotEmpty().WithMessage("Division name is required.")
            .MaximumLength(100).WithMessage("Division name must not exceed 100 characters.");
    }
}