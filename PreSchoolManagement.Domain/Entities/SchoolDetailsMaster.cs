namespace SchoolManagement.Domain.Entities;

public class SchoolDetailsMaster : BaseEntity
{
    public int SchoolId { get; set; }

    public int CommitteeId { get; set; }

    public int SchoolTypeId { get; set; }

    public string SchoolName { get; set; } = string.Empty;

    public int SectionId { get; set; }

    public string? UDISECode { get; set; }

    public string? Village { get; set; }

    public string? Taluka { get; set; }

    public int DistrictId { get; set; }

    public string? State { get; set; }

    public string? PinCode { get; set; }

    public string? SchoolRegistrationNo { get; set; }

    public string? ContactNo { get; set; }

    public string? Board { get; set; }

    public string? Email { get; set; }

    public string? Medium { get; set; }

    public string? AffiliationNo { get; set; }

    public bool Status { get; set; }
}
