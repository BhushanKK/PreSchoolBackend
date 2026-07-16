using FluentValidation;
using FluentValidation.Validators;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateMediumMasterCommandValidator : AbstractValidator<CreateMediumMasterCommand>
{
    public CreateMediumMasterCommandValidator()
    {
        RuleFor(x => x.Medium)
            .NotEmpty().WithMessage("Medium Name is required.")
            .MaximumLength(100).WithMessage("Medium Name must not exceed 100 characters.");
    }
}

public class UpdateMediumMasterCommandValidator : AbstractValidator<UpdateMediumMasterCommand>
{
    public UpdateMediumMasterCommandValidator()
    {
        RuleFor(x =>x.MediumId)
            .GreaterThan(0).WithMessage("Medium Id is required.");

        RuleFor(x =>x.Medium)
            .NotEmpty().WithMessage("Medium name is required.")
            .MaximumLength(100).WithMessage("Medium name must not exceed 100 characters.");
    }
}