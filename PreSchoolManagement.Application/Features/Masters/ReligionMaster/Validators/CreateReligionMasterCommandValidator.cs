using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateReligionMasterCommandValidator : AbstractValidator<CreateReligionMasterCommand>
{
    public CreateReligionMasterCommandValidator()
    {
        RuleFor(x => x.Religion)
            .NotEmpty().WithMessage("Religion name is required.")
            .MaximumLength(100).WithMessage("Religion name must not exceed 100 characters.");    
              
    }
}

public class UpdateReligionMasterCommandValidator : AbstractValidator<UpdateReligionMasterCommand>
{
    public UpdateReligionMasterCommandValidator()
    {
        RuleFor(x => x.ReligionId)
            .GreaterThan(0).WithMessage("ReligionId is required.");

        RuleFor(x => x.Religion)
            .NotEmpty().WithMessage("Religion name is required.")
            .MaximumLength(100).WithMessage("Religion name must not exceed 100 characters.");
       
    }
}

