namespace SchoolManagement.Domain;

public sealed class AuditContext
{
    private readonly AsyncLocal<AuditContextItem?> _current = new();
    public AuditContextItem? Current => _current.Value;
    public void Set(AuditContextItem item) => _current.Value = item;
    public void Clear() => _current.Value = null;
}

public sealed class AuditContextItem
{
    public string? UserId { get; set; }
    public string? UserName { get; set; }
    public string? RequestMethod { get; set; }
    public string? RequestPath { get; set; }
}
