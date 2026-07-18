using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateRoleMasterCommandValidator
    : AbstractValidator<CreateRoleMasterCommand>
{
    public CreateRoleMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.RoleName)
            .Required(localizer, "RoleName")
            .MaxLengthLocalized(localizer, "RoleName", 100);
    }
}

public class UpdateRoleMasterCommandValidator
    : AbstractValidator<UpdateRoleMasterCommand>
{
    public UpdateRoleMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.RoleId)
            .RequiredId(localizer, "RoleId");

        RuleFor(x => x.RoleName)
            .Required(localizer, "RoleName")
            .MaxLengthLocalized(localizer, "RoleName", 100);
    }
}