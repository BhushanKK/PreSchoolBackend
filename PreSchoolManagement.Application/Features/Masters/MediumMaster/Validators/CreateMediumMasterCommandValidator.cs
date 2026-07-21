using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateMediumMasterCommandValidator
    : AbstractValidator<CreateMediumMasterCommand>
{
    public CreateMediumMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.MediumName)
            .Required(localizer, "Medium")
            .MaxLengthLocalized(localizer, "Medium", 20);
    }
}

public class UpdateMediumMasterCommandValidator
    : AbstractValidator<UpdateMediumMasterCommand>
{
    public UpdateMediumMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.MediumId)
            .RequiredId(localizer, "MediumId");

        RuleFor(x => x.MediumName)
            .Required(localizer, "Medium")
            .MaxLengthLocalized(localizer, "Medium", 20);
    }
}