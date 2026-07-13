using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateHolidayMasterCommandValidator : AbstractValidator<CreateHolidayMasterCommand>
{
    public CreateHolidayMasterCommandValidator()
    {
        RuleFor(x => x.HolidayName)
            .NotEmpty().WithMessage("Holiday name is required.")
            .MaximumLength(100).WithMessage("Holiday name must not exceed 100 characters.");
    }
}

public class UpdateHolidayMasterCommandValidator : AbstractValidator<UpdateHolidayMasterCommand>
{
    public UpdateHolidayMasterCommandValidator()
    {
        RuleFor(x => x.HolidayId)
            .GreaterThan(0).WithMessage("HolidayID is required.");

        RuleFor(x => x.HolidayName)
            .NotEmpty().WithMessage("Holiday name is required.")
            .MaximumLength(100).WithMessage("Holiday name must not exceed 100 characters.");
    }
}