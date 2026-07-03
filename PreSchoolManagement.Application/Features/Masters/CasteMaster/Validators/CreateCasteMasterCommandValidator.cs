using FluentValidation;
using SchoolAdmission.Application.Features.Commands;


namespace SchoolAdmission.Application.Features.Masters.Validators;

public class CreateCasteMasterCommandValidator : AbstractValidator<CreateCasteMasterCommand>
{
    public CreateCasteMasterCommandValidator()
    {
        RuleFor(x => x.Caste)
            .NotEmpty().WithMessage("Caste name is required.")
            .MaximumLength(100).WithMessage("Caste name must not exceed 100 characters.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).When(x => x.CategoryId.HasValue)
            .WithMessage("CategoryId must be greater than 0 when provided.");
    }
}

public class UpdateCasteMasterCommandValidator : AbstractValidator<UpdateCasteMasterCommand>
{
    public UpdateCasteMasterCommandValidator()
    {
        RuleFor(x => x.CasteId)
            .GreaterThan(0).WithMessage("CasteId is required.");

        RuleFor(x => x.Caste)
            .NotEmpty().WithMessage("Caste name is required.")
            .MaximumLength(100).WithMessage("Caste name must not exceed 100 characters.");

        RuleFor(x => x.CategoryId)
            .GreaterThan(0).When(x => x.CategoryId.HasValue)
            .WithMessage("CategoryId must be greater than 0 when provided.");
    }
}

