using System.Text.Json.Serialization;

namespace SchoolManagement.Domain;

public abstract class BaseEntity
{
    [JsonIgnore]
    public int? EntryBy { get; set; }

    [JsonIgnore]
    public DateTime? EntryDate { get; set; }

    [JsonIgnore]
    public int? ModifyBy { get; set; }

    [JsonIgnore]
    public DateTime? ModifyDate { get; set; }
}