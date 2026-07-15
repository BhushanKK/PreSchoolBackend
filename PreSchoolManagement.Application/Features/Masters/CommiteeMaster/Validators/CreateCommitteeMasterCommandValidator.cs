using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateCommitteeMasterCommandValidator : AbstractValidator<CreateCommitteeMasterCommand>
{
    public CreateCommitteeMasterCommandValidator()
    {
        RuleFor(x => x.CommitteeName)
            .NotEmpty().WithMessage("Committee Name is required.")
            .MaximumLength(100).WithMessage("Committee Name must not exceed 100 characters.");

        RuleFor(x => x.Slogan)
            .MaximumLength(250).WithMessage("Slogan must not exceed 250 characters.");

        RuleFor(x => x.Logo)
            .MaximumLength(500).WithMessage("Logo must not exceed 500 characters.");

        RuleFor(x => x.LogoPath)
            .MaximumLength(500).WithMessage("Logo Path must not exceed 500 characters.");
    }
}

public class UpdateCommitteeMasterCommandValidator : AbstractValidator<UpdateCommitteeMasterCommand>
{
    public UpdateCommitteeMasterCommandValidator()
    {
        RuleFor(x => x.CommitteeId)
            .GreaterThan(0).WithMessage("Committee Id is required.");

        RuleFor(x => x.CommitteeName)
            .NotEmpty().WithMessage("Committee Name is required.")
            .MaximumLength(100).WithMessage("Committee Name must not exceed 100 characters.");

        RuleFor(x => x.Slogan)
            .MaximumLength(250).WithMessage("Slogan must not exceed 250 characters.");

        RuleFor(x => x.Logo)
            .MaximumLength(500).WithMessage("Logo must not exceed 500 characters.");

        RuleFor(x => x.LogoPath)
            .MaximumLength(500).WithMessage("Logo Path must not exceed 500 characters.");
    }
}