using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Extensions;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateAcademicYearMasterCommandValidator
    : AbstractValidator<CreateAcademicYearMasterCommand>
{
    public CreateAcademicYearMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.AcademicYearName)
            .Required(localizer, "AcademicYearName")
            .MaxLengthLocalized(localizer, "AcademicYearName", 100);

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

public class UpdateAcademicYearMasterCommandValidator
    : AbstractValidator<UpdateAcademicYearMasterCommand>
{
    public UpdateAcademicYearMasterCommandValidator(
        ILocalizationService localizer)
    {
        RuleFor(x => x.AcademicYearId)
            .RequiredId(localizer, "AcademicYearId");

        RuleFor(x => x.AcademicYearName)
            .Required(localizer, "AcademicYearName")
            .MaxLengthLocalized(localizer, "AcademicYearName", 100);

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