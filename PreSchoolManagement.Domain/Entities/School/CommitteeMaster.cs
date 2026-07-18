namespace SchoolManagement.Domain.Entities;

public class CommitteeMaster : BaseEntity
{
    public Guid CommitteeId { get; set; }
    public string CommitteeName { get; set; } = string.Empty;
    public string? Slogan { get; set; }
    public string? Logo { get; set; }
    public string? LogoPath { get; set; }
    public bool IsActive { get; set; } = false;
}