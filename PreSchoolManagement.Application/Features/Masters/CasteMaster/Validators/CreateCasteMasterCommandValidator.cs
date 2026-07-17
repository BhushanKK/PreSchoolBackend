using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateCasteMasterCommandValidator
    : AbstractValidator<CreateCasteMasterCommand>
{
    public CreateCasteMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.Caste)
            .Required(localizer, "Caste")
            .MaxLengthLocalized(localizer, "Caste", 100);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .When(x => x.CategoryId.HasValue)
            .WithMessage(
                localizer.Get(
                    "ValidationMessages",
                    "GreaterThan",
                    localizer.Get("ValidationMessages", "Category"),
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

        RuleFor(x => x.Caste)
            .Required(localizer, "Caste")
            .MaxLengthLocalized(localizer, "Caste", 100);

        RuleFor(x => x.CategoryId)
            .GreaterThan(0)
            .When(x => x.CategoryId.HasValue)
            .WithMessage(
                localizer.Get(
                    "ValidationMessages",
                    "GreaterThan",
                    localizer.Get("ValidationMessages", "Category"),
                    "0"));
    }
}