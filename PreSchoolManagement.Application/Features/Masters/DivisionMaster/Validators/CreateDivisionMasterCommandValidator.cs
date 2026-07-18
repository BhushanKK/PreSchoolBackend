using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateDivisionMasterCommandValidator
    : AbstractValidator<CreateDivisionMasterCommand>
{
    public CreateDivisionMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.DivisionName)
            .Required(localizer, "DivisionName")
            .MaxLengthLocalized(localizer, "DivisionName", 100);
    }
}

public class UpdateDivisionMasterCommandValidator
    : AbstractValidator<UpdateDivisionMasterCommand>
{
    public UpdateDivisionMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.DivisionId)
            .RequiredId(localizer, "DivisionId");

        RuleFor(x => x.DivisionName)
            .Required(localizer, "DivisionName")
            .MaxLengthLocalized(localizer, "DivisionName", 100);
    }
}