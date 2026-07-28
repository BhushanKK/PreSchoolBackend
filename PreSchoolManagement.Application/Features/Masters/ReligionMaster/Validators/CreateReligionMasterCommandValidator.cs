using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateReligionMasterCommandValidator
    : AbstractValidator<CreateReligionMasterCommand>
{
    public CreateReligionMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.ReligionName!)
            .Required(localizer, "Religion")
            .MaxLengthLocalized(localizer, "Religion", 50);
    }
}

public class UpdateReligionMasterCommandValidator
    : AbstractValidator<UpdateReligionMasterCommand>
{
    public UpdateReligionMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.ReligionId)
            .RequiredId(localizer, "ReligionId");

        RuleFor(x => x.ReligionName!)
            .Required(localizer, "Religion")
            .MaxLengthLocalized(localizer, "Religion", 50);
    }
}