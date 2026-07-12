using System.Text.Json.Serialization;

public class DistrictMasterDto
{
    [JsonIgnore]
    public int DistrictId{get; set;}

    public int? StateId{get; set;}

    public string? DistrictName {get; set;}

    public bool IsActive{get; set;}
}

public class DistrictMasterQueryDto : DistrictMasterDto
{   
    [JsonPropertyName("DistrictId")]
    public int Id 
    {
        get => DistrictId;
        set => DistrictId = value; 
    }

    public new string DistrictName { get; set;} = string.Empty;
}
