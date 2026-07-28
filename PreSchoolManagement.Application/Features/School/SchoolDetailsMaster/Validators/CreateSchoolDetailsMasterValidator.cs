using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateSchoolDetailsMasterCommandValidator
    : AbstractValidator<CreateSchoolDetailsMasterCommand>
{
    public CreateSchoolDetailsMasterCommandValidator()
    {
        RuleFor(x => x.CommitteeId)
            .NotEmpty().WithMessage("Committee Id is required.");

        RuleFor(x => x.SchoolName)
            .NotEmpty().WithMessage("School Name is required.")
            .MaximumLength(200).WithMessage("School Name must not exceed 200 characters.");

        RuleFor(x => x.SchoolNameEnglish)
            .MaximumLength(200).WithMessage("School Name English must not exceed 200 characters.");

        RuleFor(x => x.RecognitionNumber)
            .NotEmpty().WithMessage("Recognition Number is required.")
            .MaximumLength(100).WithMessage("Recognition Number must not exceed 100 characters.");

        RuleFor(x => x.UDISECode)
            .NotEmpty().WithMessage("UDISE Code is required.")
            .MaximumLength(50).WithMessage("UDISE Code must not exceed 50 characters.");

        RuleFor(x => x.SequenceNumber)
            .GreaterThan(0).WithMessage("Sequence Number must be greater than 0.");

        RuleFor(x => x.BoardId)
            .NotEmpty().WithMessage("Board is required.");

        RuleFor(x => x.MediumId)
            .NotEmpty().WithMessage("Medium is required.");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("Phone Number must not exceed 20 characters.");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Invalid Email Address.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        RuleFor(x => x.Village)
            .NotEmpty().WithMessage("Village is required.")
            .MaximumLength(100).WithMessage("Village must not exceed 100 characters.");

        RuleFor(x => x.Taluka)
            .NotEmpty().WithMessage("Taluka is required.")
            .MaximumLength(100).WithMessage("Taluka must not exceed 100 characters.");

        RuleFor(x => x.DistrictId)
            .NotEmpty().WithMessage("District is required.");

        RuleFor(x => x.StateId)
            .NotEmpty().WithMessage("State is required.");

        RuleFor(x => x.PinCode)
            .MaximumLength(10).WithMessage("Pin Code must not exceed 10 characters.");

        RuleFor(x => x.StandardIds)
            .MaximumLength(500).WithMessage("StandardIds must not exceed 500 characters.");
    }
}

public class UpdateSchoolDetailsMasterCommandValidator
    : AbstractValidator<UpdateSchoolDetailsMasterCommand>
{
    public UpdateSchoolDetailsMasterCommandValidator()
    {
        RuleFor(x => x.SchoolDetailsId)
            .NotEmpty().WithMessage("School Details Id is required.");

        RuleFor(x => x.CommitteeId)
            .NotEmpty().WithMessage("Committee Id is required.");

        RuleFor(x => x.SchoolName)
            .NotEmpty().WithMessage("School Name is required.")
            .MaximumLength(200).WithMessage("School Name must not exceed 200 characters.");

        RuleFor(x => x.SchoolNameEnglish)
            .MaximumLength(200).WithMessage("School Name English must not exceed 200 characters.");

        RuleFor(x => x.RecognitionNumber)
            .NotEmpty().WithMessage("Recognition Number is required.")
            .MaximumLength(100).WithMessage("Recognition Number must not exceed 100 characters.");

        RuleFor(x => x.UDISECode)
            .NotEmpty().WithMessage("UDISE Code is required.")
            .MaximumLength(50).WithMessage("UDISE Code must not exceed 50 characters.");

        RuleFor(x => x.SequenceNumber)
            .GreaterThan(0).WithMessage("Sequence Number must be greater than 0.");

        RuleFor(x => x.BoardId)
            .NotEmpty().WithMessage("Board is required.");

        RuleFor(x => x.MediumId)
            .NotEmpty().WithMessage("Medium is required.");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("Phone Number must not exceed 20 characters.");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Invalid Email Address.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        RuleFor(x => x.Village)
            .NotEmpty().WithMessage("Village is required.")
            .MaximumLength(100).WithMessage("Village must not exceed 100 characters.");

        RuleFor(x => x.Taluka)
            .NotEmpty().WithMessage("Taluka is required.")
            .MaximumLength(100).WithMessage("Taluka must not exceed 100 characters.");

        RuleFor(x => x.DistrictId)
            .NotEmpty().WithMessage("District is required.");

        RuleFor(x => x.StateId)
            .NotEmpty().WithMessage("State is required.");

        RuleFor(x => x.PinCode)
            .MaximumLength(10).WithMessage("Pin Code must not exceed 10 characters.");

        RuleFor(x => x.StandardIds)
            .MaximumLength(500).WithMessage("StandardIds must not exceed 500 characters.");
    }
}