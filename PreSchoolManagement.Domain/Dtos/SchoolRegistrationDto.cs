using System.Text.Json.Serialization;
using SchoolManagement.Shared.Enums;

namespace PreSchoolManagement.Domain.Dtos;

public class SchoolRegistrationDto
{
    [JsonIgnore]
    public Guid SchoolRegistrationId { get; set; }
    public Guid CommitteeId { get; set; }

    public string SchoolName { get; set; } = string.Empty;
    public string? SchoolAddress { get; set; }
    public string? Taluka { get; set; }
    public string? District { get; set; }
    public string? Pincode { get; set; }

    public string? SchoolContactNo { get; set; }
    public string? MobileNo { get; set; }
    public string? SchoolEmailId { get; set; }

    public int? SectionId { get; set; }
    public int? MediumId { get; set; }
    public int? BoardId { get; set; }

    public SchoolGrantType SchoolGrantType { get; set; }
    public SchoolManagementType SchoolManagementType { get; set; }
    public SchoolAreaType SchoolAreaType { get; set; }

    public TimeOnly? SchoolStartTime { get; set; }
    public TimeOnly? SchoolEndTime { get; set; }

    public bool FillTwice { get; set; }

    public string? SchoolAddressForDocuments { get; set; }

    // New Fields
    public string? SchoolNameInRegional { get; set; }
    public string? SchoolNameEnglish { get; set; }

    public string? RecognitionNumber { get; set; }
    public string? UDISECode { get; set; }

    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }

    public string? Village { get; set; }
    public int? StateId { get; set; }
    public string? Landmark { get; set; }

    public bool SmsFacility { get; set; }
    public bool EmailFacility { get; set; }
    public bool MobileAppFacility { get; set; }
    public bool ScholarshipFacility { get; set; }

    public DateOnly? SubscriptionValidityDate { get; set; }

    public bool IsActive { get; set; } = true;
}