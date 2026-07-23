using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateDistrictMasterCommandValidator
    : AbstractValidator<CreateDistrictMasterCommand>
{
    public CreateDistrictMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.DistrictName!)
            .Required(localizer, "DistrictName")
            .MaxLengthLocalized(localizer, "DistrictName", 100);
    }
}

public class UpdateDistrictMasterCommandValidator
    : AbstractValidator<UpdateDistrictMasterCommand>
{
    public UpdateDistrictMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.DistrictId)
            .RequiredId(localizer, "DistrictId");

        RuleFor(x => x.DistrictName!)
            .Required(localizer, "DistrictName")
            .MaxLengthLocalized(localizer, "DistrictName", 100);

        RuleFor(x => x.StateId)
            .GreaterThan(0)
            .When(x => x.StateId.HasValue)
            .WithMessage(
                localizer.Get(
                    LocaleEnums.ValidationMessages.ToString(),
                    "GreaterThan",
                    localizer.Get(LocaleEnums.ValidationMessages.ToString(), "State"),
                    "0"));
    }
}