using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateFinancialYearMasterCommandValidator : AbstractValidator<CreateFinancialYearMasterCommand>
{
    public CreateFinancialYearMasterCommandValidator()
    {
        RuleFor(x => x.FinancialYearName)
            .NotEmpty().WithMessage("Financial Year name is required.")
            .MaximumLength(100).WithMessage("Financial Year name must not exceed 100 characters.");

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

public class UpdateFinancialYearMasterCommandValidator : AbstractValidator<UpdateFinancialYearMasterCommand>
{
    public UpdateFinancialYearMasterCommandValidator()
    {
        RuleFor(x => x.FinancialYearId)
            .GreaterThan(0).WithMessage("FinancialYearId is required.");

        RuleFor(x => x.FinancialYearName)
            .NotEmpty().WithMessage("FinancialYearName is required.")
            .MaximumLength(100).WithMessage("FinancialYearName must not exceed 100 characters.");
        
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