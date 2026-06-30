using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Infrastructure.Data;

public partial class ApplicationDbContext
{
    private readonly AuditContext _auditContext;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AuditContext auditContext)
        : base(options)
    {
        _auditContext = auditContext;
    }

    public DbSet<AuditLog> AuditLogs => Set<AuditLog>();

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var auditEntries = new List<AuditLog>();
        var entries = ChangeTracker.Entries()
            .Where(e => e.Entity is not AuditLog && (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
            .ToList();

        foreach (var entry in entries)
        {
            var tableName = entry.Metadata.GetTableName() ?? entry.Entity.GetType().Name;
            var entityId = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "Id")?.CurrentValue?.ToString();

            if (entry.State == EntityState.Added)
            {
                auditEntries.Add(new AuditLog
                {
                    TableName = tableName,
                    Action = "Added",
                    EntityId = entityId,
                    NewValues = JsonSerializer.Serialize(entry.Properties.Where(p => p.Metadata.PropertyInfo != null && !p.Metadata.IsPrimaryKey()).ToDictionary(p => p.Metadata.Name, p => p.CurrentValue)),
                    UserId = _auditContext.Current?.UserId,
                    UserName = _auditContext.Current?.UserName,
                    RequestMethod = _auditContext.Current?.RequestMethod,
                    RequestPath = _auditContext.Current?.RequestPath
                });
            }
            else if (entry.State == EntityState.Modified)
            {
                var changedProperties = entry.Properties.Where(p => p.Metadata.PropertyInfo != null && p.IsModified).ToList();
                auditEntries.Add(new AuditLog
                {
                    TableName = tableName,
                    Action = "Updated",
                    EntityId = entityId,
                    OldValues = JsonSerializer.Serialize(changedProperties.ToDictionary(p => p.Metadata.Name, p => p.OriginalValue)),
                    NewValues = JsonSerializer.Serialize(changedProperties.ToDictionary(p => p.Metadata.Name, p => p.CurrentValue)),
                    ChangedColumns = string.Join(",", changedProperties.Select(p => p.Metadata.Name)),
                    UserId = _auditContext.Current?.UserId,
                    UserName = _auditContext.Current?.UserName,
                    RequestMethod = _auditContext.Current?.RequestMethod,
                    RequestPath = _auditContext.Current?.RequestPath
                });
            }
            else if (entry.State == EntityState.Deleted)
            {
                auditEntries.Add(new AuditLog
                {
                    TableName = tableName,
                    Action = "Deleted",
                    EntityId = entityId,
                    OldValues = JsonSerializer.Serialize(entry.Properties.Where(p => p.Metadata.PropertyInfo != null).ToDictionary(p => p.Metadata.Name, p => p.OriginalValue)),
                    UserId = _auditContext.Current?.UserId,
                    UserName = _auditContext.Current?.UserName,
                    RequestMethod = _auditContext.Current?.RequestMethod,
                    RequestPath = _auditContext.Current?.RequestPath
                });
            }
        }

        var result = await base.SaveChangesAsync(cancellationToken);

        if (auditEntries.Count > 0)
        {
            AuditLogs.AddRange(auditEntries);
            await base.SaveChangesAsync(cancellationToken);
        }

        return result;
    }
}
