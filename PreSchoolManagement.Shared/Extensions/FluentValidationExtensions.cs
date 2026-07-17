using System.Linq.Expressions;
using FluentValidation;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Shared.Extensions;

public static class FluentValidationExtensions
{
    public static IRuleBuilderOptions<T, string> Required<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        ILocalizationService localizer,
        string fieldKey)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(localizer.Get(
                "ValidationMessages",
                "Required",
                localizer.Get("Masters", fieldKey)));
    }

    public static IRuleBuilderOptions<T, string> MaxLengthLocalized<T>(
        this IRuleBuilder<T, string> ruleBuilder,
        ILocalizationService localizer,
        string fieldKey,
        int maxLength)
    {
        return ruleBuilder
            .MaximumLength(maxLength)
            .WithMessage(localizer.Get(
                "ValidationMessages",
                "MaxLength",
                localizer.Get("Masters", fieldKey),
                maxLength));
    }

    public static IRuleBuilderOptions<T, DateTime> RequiredDate<T>(
        this IRuleBuilder<T, DateTime> ruleBuilder,
        ILocalizationService localizer,
        string fieldKey)
    {
        return ruleBuilder
            .NotEmpty()
            .WithMessage(localizer.Get(
                "ValidationMessages",
                "Required",
                localizer.Get("Masters", fieldKey)));
    }

    public static IRuleBuilderOptions<T, DateTime> GreaterThanDate<T>(
    this IRuleBuilder<T, DateTime> ruleBuilder,
    Expression<Func<T, DateTime>> comparison,
    ILocalizationService localizer,
    string fieldKey,
    string comparisonFieldKey)
    {
        return ruleBuilder
            .GreaterThan(comparison)
            .WithMessage(localizer.Get(
                "ValidationMessages",
                "GreaterThanDate",
                localizer.Get("Masters", fieldKey),
                localizer.Get("Masters", comparisonFieldKey)));
    }

    public static IRuleBuilderOptions<T, int> RequiredId<T>(
        this IRuleBuilder<T, int> ruleBuilder,
        ILocalizationService localizer,
        string fieldKey)
    {
        return ruleBuilder
            .GreaterThan(0)
            .WithMessage(localizer.Get(
                "ValidationMessages",
                "Required",
                localizer.Get("Masters", fieldKey)));
    }
}