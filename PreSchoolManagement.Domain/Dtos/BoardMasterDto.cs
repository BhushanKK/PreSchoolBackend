using System.Text.Json.Serialization;

namespace PreSchoolManagement.Domain.Dtos;

public class BoardMasterDto
{
    [JsonIgnore]
    public int BoardId { get; set;}
    public string BoardName { get; set;}= string.Empty;
    public bool IsActive { get; set;}=false;
}