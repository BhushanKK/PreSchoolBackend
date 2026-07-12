using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class CategoryMasterDto
{
    [JsonIgnore]
    public int CategoryId { get; set; }
    public string? CategoryName { get; set; }
    public bool IsActive { get; set; } = false;
}
