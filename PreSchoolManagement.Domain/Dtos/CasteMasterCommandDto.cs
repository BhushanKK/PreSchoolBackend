using System.Text.Json.Serialization;

namespace SchoolAdmission.Domain.Dtos;

public class CasteMasterCommandDto
{
    [JsonIgnore]
    public int CasteId { get; set; }
    public int? CategoryId { get; set; }
    public string? Caste { get; set; }
}
