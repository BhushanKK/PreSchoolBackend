namespace SchoolManagement.Domain.Entities;

public class DistrictMaster : BaseEntity
{
    public int DistrictId {get; set;}
    public int StateId {get;set;}
    public  string DistrictName {get;set;} = string.Empty;
}