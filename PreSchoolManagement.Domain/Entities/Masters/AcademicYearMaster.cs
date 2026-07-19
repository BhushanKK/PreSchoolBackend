using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class AcademicYearMaster : BaseEntity
{
    public int AcademicYearId { get; set; }
    public string AcademicYearName { get; set; } = string.Empty;
    public DateTime FromDate { get; set; }
    public DateTime ToDate { get; set; }
    public bool IsActive { get; set; } = false;    
    public virtual ICollection<AcademicYearTranslation> Translations { get; set; }
        = new List<AcademicYearTranslation>();
}

public class AcademicYearTranslation
{
    public int AcademicYearTranslationId { get; set; }
    public int AcademicYearId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string AcademicYearName { get; set; } = string.Empty;
    [JsonIgnore]
    public virtual AcademicYearMaster AcademicYear { get; set; } = null!;
}