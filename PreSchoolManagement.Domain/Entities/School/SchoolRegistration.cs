using SchoolManagement.Shared.Enums;

namespace SchoolManagement.Domain.Entities;

public class SchoolRegistration : BaseEntity
{
    public Guid SchoolRegistrationId { get; set; }

    // Institute Information
    public Guid CommitteeId { get; set; }
    public string SchoolName { get; set; } = string.Empty;

    // Address
    public string? SchoolAddress { get; set; }
    public string? Taluka { get; set; }
    public string? District { get; set; }
    public string? Pincode { get; set; }

    // Contact Information
    public string? SchoolContactNo { get; set; }
    public string? MobileNo { get; set; }
    public string? SchoolEmailId { get; set; }

    // School Details
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
}