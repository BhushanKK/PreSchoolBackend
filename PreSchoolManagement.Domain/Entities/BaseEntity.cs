using System.Text.Json.Serialization;

namespace SchoolManagement.Domain;

public abstract class BaseEntity
{
    [JsonIgnore]
    public Guid? EntryBy { get; set; }

    [JsonIgnore]
    public DateTime? EntryDate { get; set; }

    [JsonIgnore]
    public Guid? ModifyBy { get; set; }

    [JsonIgnore]
    public DateTime? ModifyDate { get; set; }
    public bool IsActive { get; set; } = false;
}