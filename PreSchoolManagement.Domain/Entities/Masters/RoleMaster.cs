using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class RoleMaster : BaseEntity
{
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;
    public bool IsActive { get; set; } = false;
    [JsonIgnore]
    public virtual ICollection<RoleTranslation> Translations { get;set;} 
    = new List<RoleTranslation>();
}

public class RoleTranslation
{
    public int RoleTranslationId { get; set; }
    public int RoleId { get; set; }
    public string LanguageCode { get; set; } = string.Empty;
    public string RoleName { get; set; } = string.Empty;
    public virtual RoleMaster Role { get; set; } = null!; 
}
