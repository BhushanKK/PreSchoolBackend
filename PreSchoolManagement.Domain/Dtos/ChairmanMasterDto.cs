using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class ChairmanMasterDto
{
    [JsonIgnore]
    public int ChairmanId { get; set; }
    public int? CommitteeId { get; set; }
    public int? DesignationId { get; set; }
    public string ChairmanName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhotoPath { get; set; } = string.Empty;
    public string SignaturePath { get; set; } = string.Empty;
    public bool IsActive { get; set; }
    public ICollection<ChairmanTranslationDto> Translations { get; set; } 
    = new List<ChairmanTranslationDto>();
}

public class ChairmanMasterQueryDto : ChairmanMasterDto
{
    [JsonPropertyName("ChairmanId")]
    public int Id
    {
        get => ChairmanId;
        set => ChairmanId = value;
    }
     public int Committee_Id
    {
        get => Committee_Id;
        set => CommitteeId = value;
    }
     public int designation_Id
    {
        get => designation_Id;
        set => DesignationId = value;
    }
    public string Designation { get; set; } = string.Empty;
    public string CommitteeName { get; set; } = string.Empty;
}

public class ChairmanTranslationDto
{
    public string ChairmanName { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string PhotoPath { get; set; } = string.Empty;
    public string SignaturePath { get; set; } = string.Empty;
    public int? ChairmanTranslationId {get;set;}
    public int? ChairmanId {get;set;}
     public string LanguageCode { get; set; } = string.Empty;
}

public class ChairmanDropdownDto
{
    public int ChairmanId { get; set; }
    public string ChairmanName { get; set; } = string.Empty;
}