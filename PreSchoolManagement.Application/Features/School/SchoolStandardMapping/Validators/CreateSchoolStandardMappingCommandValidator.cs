using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;
using PreSchoolManagement.Shared.Localization;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateSchoolStandardMappingCommandValidator
    : AbstractValidator<CreateSchoolStandardMappingCommand>
{
    public CreateSchoolStandardMappingCommandValidator(ILocalizationService localizer)
    {
        RuleFor(x => x.SchoolRegistrationId)
            .NotEmpty()
            .WithMessage("School Registration is required.");

        RuleFor(x => x.StandardId)
            .GreaterThan(0)
            .WithMessage("Standard is required.");
    }
}

public class UpdateSchoolStandardMappingCommandValidator
    : AbstractValidator<UpdateSchoolStandardMappingCommand>
{
    public UpdateSchoolStandardMappingCommandValidator(ILocalizationService localizer)
    {
        RuleFor(x => x.SchoolStandardMappingId)
            .NotEmpty()
            .WithMessage("School Standard Mapping Id is required.");

        RuleFor(x => x.SchoolRegistrationId)
            .NotEmpty()
            .WithMessage("School Registration is required.");

        RuleFor(x => x.StandardId)
            .GreaterThan(0)
            .WithMessage("Standard is required.");
    }
}