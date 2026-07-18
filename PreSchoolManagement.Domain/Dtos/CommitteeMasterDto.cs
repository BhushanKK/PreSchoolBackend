using System.Text.Json.Serialization;
namespace PreSchoolManagement.Domain.Dtos;

public class CommitteeMasterDto
{   
    [JsonIgnore]
    public Guid CommitteeId { get; set; }
    public string? CommitteeName { get; set; }
    public bool IsActive { get; set; }=false;
    public string? Slogan { get; set; }
    public string? Logo { get; set; }
    public string? LogoPath { get; set; }
}