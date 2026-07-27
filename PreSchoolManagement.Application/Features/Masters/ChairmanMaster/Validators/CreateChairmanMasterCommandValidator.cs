using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateChairmanMasterCommandValidator
    : AbstractValidator<CreateChairmanMasterCommand>
{
    public CreateChairmanMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.ChairmanName)
            .Required(localizer, "ChairmanName")
            .MaxLengthLocalized(localizer, "ChairmanName", 30);

        RuleFor(x => x.DesignationId)
            .GreaterThan(0)
            .When(x => x.DesignationId.HasValue)
            .WithMessage(
                localizer.Get(
                    LocaleEnums.ValidationMessages.ToString(),
                    "GreaterThan",
                    localizer.Get(LocaleEnums.ValidationMessages.ToString(), "Designation"),
                    "0"));
    }
}

public class UpdateChairmanMasterCommandValidator
    : AbstractValidator<UpdateChairmanMasterCommand>
{
    public UpdateChairmanMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.ChairmanId)
            .RequiredId(localizer, "ChairmanId");

        RuleFor(x => x.ChairmanName)
            .Required(localizer, "ChairmanName")
            .MaxLengthLocalized(localizer, "ChairmanName", 20);

        RuleFor(x => x.DesignationId)
            .GreaterThan(0)
            .When(x => x.DesignationId.HasValue)
            .WithMessage(
                localizer.Get(
                    LocaleEnums.ValidationMessages.ToString(),
                    "GreaterThan",
                    localizer.Get(LocaleEnums.ValidationMessages.ToString(), "Designation"),
                    "0"));
    }
}