namespace SchoolManagement.Domain.Entities;

public class AuditLog
{
    public int Id { get; set; }

    public string TableName { get; set; } = string.Empty;

    public string Action { get; set; } = string.Empty;

    public string? EntityId { get; set; }

    public string? OldValues { get; set; }

    public string? NewValues { get; set; }

    public string? ChangedColumns { get; set; }

    public string? UserId { get; set; }

    public string? UserName { get; set; }

    public string? RequestMethod { get; set; }

    public string? RequestPath { get; set; }

    public DateTime Timestamp { get; set; } = DateTime.UtcNow;
}
