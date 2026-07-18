namespace SchoolManagement.Domain.Entities;

public class DesignationMaster : BaseEntity
{
    public int DesignationId {get; set;}
    public string? Designation{get; set;}= string.Empty;
    public bool Status { get; set; } = true;
}