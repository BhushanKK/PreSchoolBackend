using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateDesignationMasterCommandValidator
    : AbstractValidator<CreateDesignationMasterCommand>
{
    public CreateDesignationMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.Designation!)
            .Required(localizer, "Designation")
            .MaxLengthLocalized(localizer, "Designation", 100);
    }
}

public class UpdateDesignationMasterCommandValidator
    : AbstractValidator<UpdateDesignationMasterCommand>
{
    public UpdateDesignationMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.DesignationId)
            .RequiredId(localizer, "DesignationId");

        RuleFor(x => x.Designation!)
            .Required(localizer, "Designation")
            .MaxLengthLocalized(localizer, "Designation", 100);
    }
}