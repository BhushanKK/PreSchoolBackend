namespace SchoolManagement.Domain.Entities;

public class CommitteeMaster : BaseEntity
{
    public int CommitteeId { get; set; }

    public string CommitteeName { get; set; } = string.Empty;

    public bool Status { get; set; }=true;

    public string? Slogan { get; set; }

    public string? Logo { get; set; }

    public string? LogoPath { get; set; }
}
