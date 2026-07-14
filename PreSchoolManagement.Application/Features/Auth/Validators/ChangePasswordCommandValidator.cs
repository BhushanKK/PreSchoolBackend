using FluentValidation;
using PreSchoolManagement.Application.Features.Auth.Commands;

namespace PreSchoolManagement.Application.Features.Auth.Validators;

public class ChangePasswordCommandValidator
    : AbstractValidator<ChangePasswordCommand>
{
    public ChangePasswordCommandValidator()
    {
        RuleFor(x => x.CurrentPassword)
            .NotEmpty();

        RuleFor(x => x.NewPassword)
            .NotEmpty()
            .MinimumLength(8);

        RuleFor(x => x.ConfirmPassword)
            .Equal(x => x.NewPassword)
            .WithMessage("New password and confirm password do not match.");
    }
}