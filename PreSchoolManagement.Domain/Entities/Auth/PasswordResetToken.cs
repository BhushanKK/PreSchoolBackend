namespace SchoolManagement.Domain.Entities;

public class PasswordResetToken
{
    public int PasswordResetTokenId { get; set; }
    public Guid UserId { get; set; }
    public string Token { get; set; } = string.Empty; //TokenHash
    public DateTime ExpiryDate { get; set; }
    public bool IsUsed { get; set; }
    public DateTime CreatedDate { get; set; }
    public DateTime? UsedDate { get; set; }
    public virtual UserDetailsMaster User { get; set; } = null!;
}