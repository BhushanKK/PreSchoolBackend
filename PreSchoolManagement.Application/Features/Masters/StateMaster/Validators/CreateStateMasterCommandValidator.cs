using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateStateMasterCommandValidator
    : AbstractValidator<CreateStateMasterCommand>
{
    public CreateStateMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.StateName)
            .Required(localizer, "StateName")
            .MaxLengthLocalized(localizer, "StateName", 20);
    }
}

public class UpdateStateMasterCommandValidator
    : AbstractValidator<UpdateStateMasterCommand>
{
    public UpdateStateMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.StateId)
            .RequiredId(localizer, "StateId");

        RuleFor(x => x.StateName)
            .Required(localizer, "StateName")
            .MaxLengthLocalized(localizer, "StateName", 20);
    }
}