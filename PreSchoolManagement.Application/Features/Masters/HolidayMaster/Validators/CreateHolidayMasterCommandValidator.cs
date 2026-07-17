using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateHolidayMasterCommandValidator
    : AbstractValidator<CreateHolidayMasterCommand>
{
    public CreateHolidayMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.HolidayName!)
            .Required(localizer, "HolidayName")
            .MaxLengthLocalized(localizer, "HolidayName", 100);
    }
}

public class UpdateHolidayMasterCommandValidator
    : AbstractValidator<UpdateHolidayMasterCommand>
{
    public UpdateHolidayMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.HolidayId)
            .RequiredId(localizer, "HolidayId");

        RuleFor(x => x.HolidayName!)
            .Required(localizer, "HolidayName")
            .MaxLengthLocalized(localizer, "HolidayName", 100);
    }
}