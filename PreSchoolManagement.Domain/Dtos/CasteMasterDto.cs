using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class CasteMasterDto
{
    [JsonIgnore]
    public int CasteId { get; set; }
    public int? CategoryId { get; set; }
    public string? Caste { get; set; }
}
