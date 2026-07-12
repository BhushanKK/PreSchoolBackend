using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateStandardMasterCommandValidator : AbstractValidator<CreateStandardMasterCommand>
{
    public CreateStandardMasterCommandValidator()
    {
        RuleFor(x => x.StandardName)
            .NotEmpty().WithMessage("Standard name is required.")
            .MaximumLength(100).WithMessage("Standard name must not exceed 100 characters.");
    }
}

public class UpdateStandardMasterCommandValidator : AbstractValidator<UpdateStandardMasterCommand>
{
    public UpdateStandardMasterCommandValidator()
    {
        RuleFor(x => x.StandardId)
            .GreaterThan(0).WithMessage("StandardID is required.");

        RuleFor(x => x.StandardName)
            .NotEmpty().WithMessage("Standard name is required.")
            .MaximumLength(100).WithMessage("Standard name must not exceed 100 characters.");
    }
}