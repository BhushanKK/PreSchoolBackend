using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateCategoryMasterCommandValidator : AbstractValidator<CreateCategoryMasterCommand>
{
    public CreateCategoryMasterCommandValidator()
    {
        RuleFor(x => x.CategoryName)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(50).WithMessage("Category name must not exceed 50 characters.");
    }
}

public class UpdateCategoryMasterCommandValidator : AbstractValidator<UpdateCategoryMasterCommand>
{
    public UpdateCategoryMasterCommandValidator()
    {
        RuleFor(x => x.CategoryId)
            .GreaterThan(0).WithMessage("CategoryId is required.");

        RuleFor(x => x.CategoryName)
            .NotEmpty().WithMessage("Category name is required.")
            .MaximumLength(50).WithMessage("Category name must not exceed 50 characters.");
    }
}