namespace SchoolManagement.Domain.Entities;

public class UserDetailsMaster : BaseEntity
{
    public Guid UserId { get; set; }
    public string UserName { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string? MobileNumber { get; set; }
    public string? AccessToken { get; set; }
    public string PasswordHash { get; set; } = string.Empty;
    public string? PasswordSalt { get; set; }
    public int RoleId { get; set; }
    public bool IsDeleted { get; set; }
    public int FailedLoginAttempts { get; set; }
    public DateTime? LockoutEnd { get; set; }
    public DateTime? LastLoginDate { get; set; }
    public int JwtTokenVersion { get; set; }
    public DateTime? LastFailedLoginDate {get;set;}
}
