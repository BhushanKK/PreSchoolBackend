namespace PreSchoolManagement.Infrastructure.Interfaces;

public interface ICurrentUserService
{
    public Guid? UserId { get; }
    string? UserName { get; }
}