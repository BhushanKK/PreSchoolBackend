using System.Text.Json.Serialization;

namespace SchoolManagement.Domain.Entities;

public class StateMaster : BaseEntity
{
    public int StateId { get; set;}
    public string StateName { get; set;} = string.Empty;
    public bool IsActive { get; set; } = false;
    public virtual ICollection<StateTranslation> Translations {get;set;}
    = new List<StateTranslation>();
}

public class StateTranslation
{
    public int StateTranslationId {get;set;}
    public int StateId {get;set;}
    public string LanguageCode {get; set;} = string.Empty;
    public string StateName {get; set;}=string.Empty;
    [JsonIgnore]
    public virtual StateMaster State {get; set;}= null!;
}