using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateCasteMasterCommandValidator
    : AbstractValidator<CreateCasteMasterCommand>
{
    public CreateCasteMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.CasteName)
            .Required(localizer, "CasteName")
            .MaxLengthLocalized(localizer, "CasteName", 30);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .When(x => x.CategoryId.HasValue)
            .WithMessage(
                localizer.Get(
                    LocaleEnums.ValidationMessages.ToString(),
                    "GreaterThan",
                    localizer.Get(LocaleEnums.ValidationMessages.ToString(), "Category"),
                    "0"));
    }
}

public class UpdateCasteMasterCommandValidator
    : AbstractValidator<UpdateCasteMasterCommand>
{
    public UpdateCasteMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.CasteId)
            .RequiredId(localizer, "CasteId");

        RuleFor(x => x.CasteName)
            .Required(localizer, "CasteName")
            .MaxLengthLocalized(localizer, "CasteName", 20);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .When(x => x.CategoryId.HasValue)
            .WithMessage(
                localizer.Get(
                    LocaleEnums.ValidationMessages.ToString(),
                    "GreaterThan",
                    localizer.Get(LocaleEnums.ValidationMessages.ToString(), "Category"),
                    "0"));
    }
}