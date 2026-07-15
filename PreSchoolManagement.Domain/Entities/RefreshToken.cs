namespace SchoolManagement.Domain.Entities;

public class RefreshToken
{
    public long RefreshTokenId { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty;
    public DateTime ExpiryDate { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? RevokedDate { get; set; }
    public bool IsRevoked { get; set; }
    public string? DeviceName { get; set; }
    public string? IPAddress { get; set; }
    public string? UserAgent { get; set; }
    public virtual UserDetailsMaster UserDetailsMasters { get; set; } = null!;
}