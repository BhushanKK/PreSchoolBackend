using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateStateMasterCommandValidator : AbstractValidator<CreateStateMasterCommand>
{
    public CreateStateMasterCommandValidator()
    {
        RuleFor(x => x.StateName)
            .NotEmpty().WithMessage("State name is required.")
            .WithMessage("State name must not exceed 50 characters");
    }
}

public class UpdateStateMasterCommandValidator : AbstractValidator<UpdateStateMasterCommand>
{
    public UpdateStateMasterCommandValidator()
    {
        RuleFor(x => x.StateId)
            .GreaterThan(0).WithMessage("State Id required.");

        RuleFor(x => x.StateName)
            .NotEmpty().WithMessage("State name is required.")
            .MaximumLength(50).WithMessage("State name must not exceed 50 characters.");
    }
}