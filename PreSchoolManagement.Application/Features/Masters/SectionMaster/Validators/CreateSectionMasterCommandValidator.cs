using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateSectionMasterCommandValidator : AbstractValidator<CreateSectionMasterCommand>
{
    public CreateSectionMasterCommandValidator()
    {
        RuleFor(x => x.SectionName)
            .NotEmpty().WithMessage("Section name is required.")
            .MaximumLength(100).WithMessage("Section name must not exceed 100 characters.");
    }
}

public class UpdateSectionMasterCommandValidator : AbstractValidator<UpdateSectionMasterCommand>
{
    public UpdateSectionMasterCommandValidator()
    {
        RuleFor(x => x.SectionId)
            .GreaterThan(0).WithMessage("SectionID is required.");

        RuleFor(x => x.SectionName)
            .NotEmpty().WithMessage("Section name is required.")
            .MaximumLength(100).WithMessage("Section name must not exceed 100 characters.");
    }
}