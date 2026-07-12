namespace SchoolManagement.Domain.Entities;

public class SectionMaster : BaseEntity
{
    public int SectionId { get; set; }
    public string SectionName { get; set; } = string.Empty;
}
