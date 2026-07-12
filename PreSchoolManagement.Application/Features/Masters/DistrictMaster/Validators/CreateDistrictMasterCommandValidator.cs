using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateDistrictMasterCommandValidator : AbstractValidator<CreateDistrictMasterCommand>
{
    public CreateDistrictMasterCommandValidator()
    {
        RuleFor(x => x.DistrictName)
            .NotEmpty().WithMessage("District name is required.")
            .MaximumLength(100).WithMessage("District name must not exceed 100 characters.");

    }
}

public class UpdateDistrictMasterCommandValidator : AbstractValidator<UpdateDistrictMasterCommand>
{
    public UpdateDistrictMasterCommandValidator()
    {
        RuleFor(x => x.DistrictId)
            .GreaterThan(0).WithMessage("DistrictId is required.");

        RuleFor(x => x.DistrictName)
            .NotEmpty().WithMessage("District name is required")
            .MaximumLength(100).WithMessage("District name must not be exceed 100 chatacter");

        RuleFor(x => x.StateId)
            .GreaterThan(0).When(x => x.StateId.HasValue)
            .WithMessage("DistrictId must be greater than 0 when provided.");
    }
}