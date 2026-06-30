namespace SchoolManagement.Domain.Entities;

public class CommiteeMaster : BaseEntity
{
    public int CommitteeId { get; set; }

    public string CommitteeName { get; set; } = string.Empty;

    public bool Status { get; set; }

    public string? Slogan { get; set; }

    public string? Logo { get; set; }

    public string? LogoPath { get; set; }
}
