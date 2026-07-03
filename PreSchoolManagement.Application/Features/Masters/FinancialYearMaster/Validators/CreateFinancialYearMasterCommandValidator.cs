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
    }
}