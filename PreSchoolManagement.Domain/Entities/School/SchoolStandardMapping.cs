namespace SchoolManagement.Domain.Entities;

public class SchoolStandardMapping : BaseEntity
{
    public Guid SchoolStandardMappingId {get;set;}

    public Guid SchoolRegistrationId {get;set;}

    public int StandardId {get;set;}

    public bool IsActive {get;set;} = false;
}