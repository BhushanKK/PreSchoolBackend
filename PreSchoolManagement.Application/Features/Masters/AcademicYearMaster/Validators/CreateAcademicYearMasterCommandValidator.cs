using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateAcademicYearMasterCommandValidator : AbstractValidator<CreateAcademicYearMasterCommand>
{
    public CreateAcademicYearMasterCommandValidator()
    {
        RuleFor(x => x.AcademicYearName)
            .NotEmpty().WithMessage("Academic Year name is required.")
            .MaximumLength(100).WithMessage("Academic Year name must not exceed 100 characters.");

        RuleFor(x => x.FromDate)
            .NotEmpty()
            .WithMessage("From Date is required.");

        RuleFor(x => x.ToDate)
            .NotEmpty()
            .WithMessage("To Date is required.")
            .GreaterThan(x => x.FromDate)
            .WithMessage("To Date must be later than From Date.");
    }
}

public class UpdateAcademicYearMasterCommandValidator : AbstractValidator<UpdateAcademicYearMasterCommand>
{
    public UpdateAcademicYearMasterCommandValidator()
    {
        RuleFor(x => x.AcademicYearId)
            .GreaterThan(0).WithMessage("AcademicYearId is required.");

        RuleFor(x => x.AcademicYearName)
            .NotEmpty().WithMessage("AcademicYearName is required.")
            .MaximumLength(100).WithMessage("AcademicYearName must not exceed 100 characters.");

        RuleFor(x => x.FromDate)
            .NotEmpty()
            .WithMessage("From Date is required.");

        RuleFor(x => x.ToDate)
            .NotEmpty()
            .WithMessage("To Date is required.")
            .GreaterThan(x => x.FromDate)
            .WithMessage("To Date must be later than From Date.");
    }
}