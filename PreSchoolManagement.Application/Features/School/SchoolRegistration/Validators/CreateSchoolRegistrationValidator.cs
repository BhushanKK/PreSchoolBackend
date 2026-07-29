using FluentValidation;
using PreSchoolManagement.Application.Features.Commands;

namespace PreSchoolManagement.Application.Features.Masters.Validators;

public class CreateSchoolRegistrationCommandValidator
    : AbstractValidator<CreateSchoolRegistrationCommand>
{
    public CreateSchoolRegistrationCommandValidator()
    {
        RuleFor(x => x.CommitteeId)
            .NotEmpty().WithMessage("Committee Id is required.");

        RuleFor(x => x.SchoolName)
            .NotEmpty().WithMessage("School Name is required.")
            .MaximumLength(200).WithMessage("School Name must not exceed 200 characters.");

        RuleFor(x => x.SchoolAddress)
            .MaximumLength(500).WithMessage("School Address must not exceed 500 characters.");

        RuleFor(x => x.Taluka)
            .MaximumLength(100).WithMessage("Taluka must not exceed 100 characters.");

        RuleFor(x => x.District)
            .MaximumLength(100).WithMessage("District must not exceed 100 characters.");

        RuleFor(x => x.Pincode)
            .MaximumLength(10).WithMessage("Pincode must not exceed 10 characters.");

        RuleFor(x => x.SchoolContactNo)
            .MaximumLength(20).WithMessage("School Contact Number must not exceed 20 characters.");

        RuleFor(x => x.MobileNo)
            .MaximumLength(20).WithMessage("Mobile Number must not exceed 20 characters.");

        RuleFor(x => x.SchoolEmailId)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.SchoolEmailId))
            .WithMessage("Invalid School Email Id.")
            .MaximumLength(100).WithMessage("School Email Id must not exceed 100 characters.");

        RuleFor(x => x.SectionId)
            .NotEmpty().WithMessage("Section is required.");

        RuleFor(x => x.MediumId)
            .NotEmpty().WithMessage("Medium is required.");

        RuleFor(x => x.BoardId)
            .NotEmpty().WithMessage("Board is required.");

        RuleFor(x => x.SchoolAddressForDocuments)
            .MaximumLength(500).WithMessage("School Address For Documents must not exceed 500 characters.");

        RuleFor(x => x.SchoolNameInRegional)
            .MaximumLength(200).WithMessage("School Name In Regional must not exceed 200 characters.");

        RuleFor(x => x.SchoolNameEnglish)
            .MaximumLength(200).WithMessage("School Name English must not exceed 200 characters.");

        RuleFor(x => x.RecognitionNumber)
            .MaximumLength(100).WithMessage("Recognition Number must not exceed 100 characters.");

        RuleFor(x => x.UDISECode)
            .MaximumLength(50).WithMessage("UDISE Code must not exceed 50 characters.");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("Phone Number must not exceed 20 characters.");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Invalid Email Address.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        RuleFor(x => x.Village)
            .MaximumLength(100).WithMessage("Village must not exceed 100 characters.");

        RuleFor(x => x.StateId)
            .NotEmpty().WithMessage("State is required.");

        RuleFor(x => x.Landmark)
            .MaximumLength(250).WithMessage("Landmark must not exceed 250 characters.");
    }
}

public class UpdateSchoolRegistrationCommandValidator
    : AbstractValidator<UpdateSchoolRegistrationCommand>
{
    public UpdateSchoolRegistrationCommandValidator()
    {
        RuleFor(x => x.SchoolRegistrationId)
            .NotEmpty().WithMessage("School Registration Id is required.");

        RuleFor(x => x.CommitteeId)
            .NotEmpty().WithMessage("Committee Id is required.");

        RuleFor(x => x.SchoolName)
            .NotEmpty().WithMessage("School Name is required.")
            .MaximumLength(200).WithMessage("School Name must not exceed 200 characters.");

        RuleFor(x => x.SchoolAddress)
            .MaximumLength(500).WithMessage("School Address must not exceed 500 characters.");

        RuleFor(x => x.Taluka)
            .MaximumLength(100).WithMessage("Taluka must not exceed 100 characters.");

        RuleFor(x => x.District)
            .MaximumLength(100).WithMessage("District must not exceed 100 characters.");

        RuleFor(x => x.Pincode)
            .MaximumLength(10).WithMessage("Pincode must not exceed 10 characters.");

        RuleFor(x => x.SchoolContactNo)
            .MaximumLength(20).WithMessage("School Contact Number must not exceed 20 characters.");

        RuleFor(x => x.MobileNo)
            .MaximumLength(20).WithMessage("Mobile Number must not exceed 20 characters.");

        RuleFor(x => x.SchoolEmailId)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.SchoolEmailId))
            .WithMessage("Invalid School Email Id.")
            .MaximumLength(100).WithMessage("School Email Id must not exceed 100 characters.");

        RuleFor(x => x.SectionId)
            .NotEmpty().WithMessage("Section is required.");

        RuleFor(x => x.MediumId)
            .NotEmpty().WithMessage("Medium is required.");

        RuleFor(x => x.BoardId)
            .NotEmpty().WithMessage("Board is required.");

        RuleFor(x => x.SchoolAddressForDocuments)
            .MaximumLength(500).WithMessage("School Address For Documents must not exceed 500 characters.");

        RuleFor(x => x.SchoolNameInRegional)
            .MaximumLength(200).WithMessage("School Name In Regional must not exceed 200 characters.");

        RuleFor(x => x.SchoolNameEnglish)
            .MaximumLength(200).WithMessage("School Name English must not exceed 200 characters.");

        RuleFor(x => x.RecognitionNumber)
            .MaximumLength(100).WithMessage("Recognition Number must not exceed 100 characters.");

        RuleFor(x => x.UDISECode)
            .MaximumLength(50).WithMessage("UDISE Code must not exceed 50 characters.");

        RuleFor(x => x.PhoneNumber)
            .MaximumLength(20).WithMessage("Phone Number must not exceed 20 characters.");

        RuleFor(x => x.Email)
            .EmailAddress().When(x => !string.IsNullOrWhiteSpace(x.Email))
            .WithMessage("Invalid Email Address.")
            .MaximumLength(100).WithMessage("Email must not exceed 100 characters.");

        RuleFor(x => x.Village)
            .MaximumLength(100).WithMessage("Village must not exceed 100 characters.");

        RuleFor(x => x.StateId)
            .NotEmpty().WithMessage("State is required.");

        RuleFor(x => x.Landmark)
            .MaximumLength(250).WithMessage("Landmark must not exceed 250 characters.");
    }
}