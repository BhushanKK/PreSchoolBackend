using FluentValidation;
using PreSchoolManagement.Application.Features.Auth.Commands;

namespace PreSchoolManagement.Application.Features.Auth.Validators;

public class ForgotPasswordValidator
    : AbstractValidator<ForgotPasswordCommand>
{
    public ForgotPasswordValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty()
            .EmailAddress();
    }
}