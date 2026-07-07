namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface ICurrentUserService
{
    public Guid? UserId { get; }
    string? UserName { get; }
    public int RoleId {get;}
}