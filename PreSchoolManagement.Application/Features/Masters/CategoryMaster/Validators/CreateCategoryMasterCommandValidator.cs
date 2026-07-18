using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateCategoryMasterCommandValidator
    : AbstractValidator<CreateCategoryMasterCommand>
{
    public CreateCategoryMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.CategoryName!)
            .Required(localizer, "CategoryName")
            .MaxLengthLocalized(localizer, "CategoryName", 50);
    }
}

public class UpdateCategoryMasterCommandValidator
    : AbstractValidator<UpdateCategoryMasterCommand>
{
    public UpdateCategoryMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.CategoryId)
            .RequiredId(localizer, "CategoryId");

        RuleFor(x => x.CategoryName!)
            .Required(localizer, "CategoryName")
            .MaxLengthLocalized(localizer, "CategoryName", 50);
    }
}