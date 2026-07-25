namespace SchoolManagement.Domain.Entities;
public class SchoolDetailsMaster : BaseEntity
{
    public Guid SchoolDetailsId { get; set; }
    public Guid InstitutionId { get; set; }
    public string SchoolName { get; set; } = string.Empty;
    public string? SchoolNameEnglish { get; set; }
    public Guid? DivisionId { get; set; }
    public string RecognitionNumber { get; set; } = string.Empty;
    public string UDISECode { get; set; } = string.Empty;
    public int SequenceNumber { get; set; }
    public Guid BoardId { get; set; }
    public Guid MediumId { get; set; }
    public string? PhoneNumber { get; set; }
    public string? Email { get; set; }
    public string Village { get; set; } = string.Empty;
    public string Taluka { get; set; } = string.Empty;
    public Guid DistrictId { get; set; }
    public Guid StateId { get; set; }
    public string? PinCode { get; set; }

    public bool SmsFacility { get; set; }
    public bool EmailFacility { get; set; }
    public bool MobileAppFacility { get; set; }
    public bool ScholarshipFacility { get; set; }

    public DateOnly? SubscriptionValidityDate { get; set; }

    public string? StandardIds { get; set; }

    public bool IsActive { get; set; }

}