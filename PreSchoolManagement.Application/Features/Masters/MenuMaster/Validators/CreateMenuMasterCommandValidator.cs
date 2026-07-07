using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateMenuMasterCommandValidator : AbstractValidator<CreateMenuMasterCommand>
{
    public CreateMenuMasterCommandValidator()
    {
        RuleFor(x => x.MenuName)
            .NotEmpty().WithMessage("Menu name is required.")
            .MaximumLength(100).WithMessage("Menu name must not exceed 100 characters.");

        RuleFor(x => x.MenuUrl)
            .MaximumLength(200).WithMessage("Menu URL must not exceed 200 characters.");

        RuleFor(x => x.Icon)
            .MaximumLength(100).WithMessage("Icon must not exceed 100 characters.");

        RuleFor(x => x.DisplayOrder)
            .GreaterThan(0).WithMessage("Display order must be greater than zero.");

        RuleFor(x => x.ParentMenuId)
            .Must((model, parentMenuId) => !parentMenuId.HasValue || parentMenuId != model.MenuId)
            .WithMessage("A menu cannot be its own parent.");
    }
}

public class UpdateMenuMasterCommandValidator : AbstractValidator<UpdateMenuMasterCommand>
{
    public UpdateMenuMasterCommandValidator()
    {
        RuleFor(x => x.MenuId)
            .GreaterThan(0).WithMessage("MenuId is required.");

        RuleFor(x => x.MenuName)
            .NotEmpty().WithMessage("Menu name is required.")
            .MaximumLength(100).WithMessage("Menu name must not exceed 100 characters.");

        RuleFor(x => x.MenuUrl)
            .MaximumLength(200).WithMessage("Menu URL must not exceed 200 characters.");

        RuleFor(x => x.Icon)
            .MaximumLength(100).WithMessage("Icon must not exceed 100 characters.");

        RuleFor(x => x.DisplayOrder)
            .GreaterThan(0).WithMessage("Display order must be greater than zero.");

        RuleFor(x => x.ParentMenuId)
            .Must((model, parentMenuId) => !parentMenuId.HasValue || parentMenuId != model.MenuId)
            .WithMessage("A menu cannot be its own parent.");
    }
}