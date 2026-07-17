using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;
public class CreateFinancialYearMasterCommandValidator
    : AbstractValidator<CreateFinancialYearMasterCommand>
{
    public CreateFinancialYearMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.FinancialYearName)
            .Required(localizer, "FinancialYearName")
            .MaxLengthLocalized(localizer, "FinancialYearName", 100);

        RuleFor(x => x.FromDate)
            .RequiredDate(localizer, "FromDate");

        RuleFor(x => x.ToDate)
            .RequiredDate(localizer, "ToDate")
            .GreaterThanDate(
                x => x.FromDate,
                localizer,
                "ToDate",
                "FromDate");
    }
}

public class UpdateFinancialYearMasterCommandValidator
    : AbstractValidator<UpdateFinancialYearMasterCommand>
{
    public UpdateFinancialYearMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.FinancialYearId)
            .RequiredId(localizer, "FinancialYearId");

        RuleFor(x => x.FinancialYearName)
            .Required(localizer, "FinancialYearName")
            .MaxLengthLocalized(localizer, "FinancialYearName", 100);

        RuleFor(x => x.FromDate)
            .RequiredDate(localizer, "FromDate");

        RuleFor(x => x.ToDate)
            .RequiredDate(localizer, "ToDate")
            .GreaterThanDate(
                x => x.FromDate,
                localizer,
                "ToDate",
                "FromDate");
    }
}