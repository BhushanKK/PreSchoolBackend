namespace SchoolManagement.Domain.Entities;

public class ReligionMaster : BaseEntity
{
    public int ReligionId { get; set; }
    public string Religion { get; set; } = string.Empty;
    public bool IsMinority { get; set; }
    public bool IsActive { get; set; } = false;
}