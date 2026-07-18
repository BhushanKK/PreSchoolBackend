using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateBoardMasterCommandValidator
    : AbstractValidator<CreateBoardMasterCommand>
{
    public CreateBoardMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.BoardName)
            .Required(localizer, "BoardName")
            .MaxLengthLocalized(localizer, "BoardName", 100);
    }
}

public class UpdateBoardMasterCommandValidator
    : AbstractValidator<UpdateBoardMasterCommand>
{
    public UpdateBoardMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.BoardId)
            .RequiredId(localizer, "BoardId");

        RuleFor(x => x.BoardName)
            .Required(localizer, "BoardName")
            .MaxLengthLocalized(localizer, "BoardName", 100);
    }
}