using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateSectionMasterCommandValidator
    : AbstractValidator<CreateSectionMasterCommand>
{
    public CreateSectionMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.SectionName)
            .Required(localizer, "SectionName")
            .MaxLengthLocalized(localizer, "SectionName", 20);
    }
}

public class UpdateSectionMasterCommandValidator
    : AbstractValidator<UpdateSectionMasterCommand>
{
    public UpdateSectionMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.SectionId)
            .RequiredId(localizer, "SectionId");

        RuleFor(x => x.SectionName)
            .Required(localizer, "SectionName")
            .MaxLengthLocalized(localizer, "SectionName", 20);
    }
}