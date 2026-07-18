namespace SchoolManagement.Domain.Entities;
public class MediumMaster : BaseEntity
{
    public int MediumId{get;set;}
    public string Medium{get; set;}= string.Empty;
}