using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateStandardMasterCommandValidator
    : AbstractValidator<CreateStandardMasterCommand>
{
    public CreateStandardMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.StandardName)
            .Required(localizer, "StandardName")
            .MaxLengthLocalized(localizer, "StandardName", 20);
    }
}

public class UpdateStandardMasterCommandValidator
    : AbstractValidator<UpdateStandardMasterCommand>
{
    public UpdateStandardMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.StandardId)
            .RequiredId(localizer, "StandardId");

        RuleFor(x => x.StandardName)
            .Required(localizer, "StandardName")
            .MaxLengthLocalized(localizer, "StandardName", 20);
    }
}