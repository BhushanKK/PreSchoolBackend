using System.Linq.Expressions;
using FluentValidation;
using PreSchoolManagement.Domain.Utils;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Shared.Extensions;

public static class FluentValidationExtensions
{
    private const string FieldsResource = "Fields";

    public static IRuleBuilderOptions<T, string> Required<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        ILocalizationService localizer,
        string fieldKey,
        string fieldResource = FieldsResource)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(localizer.Get(
                LocaleEnums.ValidationMessages.ToString(),
                "Required",
                localizer.Get(fieldResource, fieldKey)));
    }

    public static IRuleBuilderOptions<T, string> MaxLengthLocalized<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        ILocalizationService localizer,
        string fieldKey,
        int maxLength,
        string fieldResource = FieldsResource)
    {
        return ruleBuilder
            .MaximumLength(maxLength)
            .WithMessage(localizer.Get(
                LocaleEnums.ValidationMessages.ToString(),
                "MaxLength",
                localizer.Get(fieldResource, fieldKey),
                maxLength));
    }

    public static IRuleBuilderOptions<T, DateTime> RequiredDate<T>(
        this IRuleBuilder<T, DateTime> ruleBuilder,
        ILocalizationService localizer,
        string fieldKey,
        string fieldResource = FieldsResource)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(localizer.Get(
                LocaleEnums.ValidationMessages.ToString(),
                "Required",
                localizer.Get(fieldResource, fieldKey)));
    }

    public static IRuleBuilderOptions<T, DateTime> GreaterThanDate<T>(
        this IRuleBuilder<T, DateTime> ruleBuilder,
        Expression<Func<T, DateTime>> comparison,
        ILocalizationService localizer,
        string fieldKey,
        string comparisonFieldKey,
        string fieldResource = FieldsResource)
    {
        return ruleBuilder
            .GreaterThan(comparison)
            .WithMessage(localizer.Get(
                LocaleEnums.ValidationMessages.ToString(),
                "GreaterThanDate",
                localizer.Get(fieldResource, fieldKey),
                localizer.Get(fieldResource, comparisonFieldKey)));
    }

    public static IRuleBuilderOptions<T, int> RequiredId<T>(
        this IRuleBuilder<T, int> ruleBuilder,
        ILocalizationService localizer,
        string fieldKey,
        string fieldResource = FieldsResource)
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage(localizer.Get(
                LocaleEnums.ValidationMessages.ToString(),
                "Required",
                localizer.Get(fieldResource, fieldKey)));
    }
}