using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;
public class CreateBoardMasterCommandValidator : AbstractValidator<CreateBoardMasterCommand>
{
    public CreateBoardMasterCommandValidator()
    {
        RuleFor(x =>x.BoardName)
            .NotEmpty().WithMessage("Board Name is required.")
            .MaximumLength(100).WithMessage("Board Name must not exceed 100 characters.");
    }
}

public class UpdateBoardMasterCommandValidator : AbstractValidator<UpdateBoardMasterCommand>
{
    public UpdateBoardMasterCommandValidator()
    {
        RuleFor(x =>x.BoardId)
            .GreaterThan(0).WithMessage("Board Id id required.");

        RuleFor(x => x.BoardName)
            .NotEmpty().WithMessage("Board Name is required.")
            .MaximumLength(100).WithMessage("Board Name must not exceed 100 characters.");
    }
}