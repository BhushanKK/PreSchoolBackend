using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using SchoolManagement.Domain;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data;

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
            .Where(e => e.Entity is not AuditLog &&
                        (e.State == EntityState.Added || e.State == EntityState.Modified || e.State == EntityState.Deleted))
            .ToList();

        foreach (var entry in entries)
        {
            var tableName = entry.Metadata.GetTableName() ?? entry.Entity.GetType().Name;
            var entityId = entry.Properties.FirstOrDefault(p => p.Metadata.Name == "Id")?.CurrentValue?.ToString();

            switch (entry.State)
            {
                case EntityState.Added:
                    auditEntries.Add(CreateAuditEntry(
                        tableName,
                        "Added",
                        entityId,
                        newValues: SerializePropertyValues(entry.Properties.Where(p => p.Metadata.PropertyInfo != null && !p.Metadata.IsPrimaryKey()), p => p.CurrentValue)));
                    break;

                case EntityState.Modified:
                    var changedProperties = entry.Properties.Where(p => p.Metadata.PropertyInfo != null && p.IsModified).ToList();
                    auditEntries.Add(CreateAuditEntry(
                        tableName,
                        "Updated",
                        entityId,
                        oldValues: SerializePropertyValues(changedProperties, p => p.OriginalValue),
                        newValues: SerializePropertyValues(changedProperties, p => p.CurrentValue),
                        changedColumns: string.Join(",", changedProperties.Select(p => p.Metadata.Name))));
                    break;

                case EntityState.Deleted:
                    auditEntries.Add(CreateAuditEntry(
                        tableName,
                        "Deleted",
                        entityId,
                        oldValues: SerializePropertyValues(entry.Properties.Where(p => p.Metadata.PropertyInfo != null), p => p.OriginalValue)));
                    break;
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

    private AuditLog CreateAuditEntry(
        string tableName,
        string action,
        string? entityId,
        string? oldValues = null,
        string? newValues = null,
        string? changedColumns = null)
    {
        return new AuditLog
        {
            TableName = tableName,
            Action = action,
            EntityId = entityId,
            OldValues = oldValues,
            NewValues = newValues,
            ChangedColumns = changedColumns,
            UserId = _auditContext.Current?.UserId,
            UserName = _auditContext.Current?.UserName,
            RequestMethod = _auditContext.Current?.RequestMethod,
            RequestPath = _auditContext.Current?.RequestPath
        };
    }

    private static string? SerializePropertyValues(IEnumerable<PropertyEntry> properties, Func<PropertyEntry, object?> valueSelector)
    {
        var values = properties
            .Where(p => p.Metadata.PropertyInfo != null)
            .ToDictionary(p => p.Metadata.Name, p => valueSelector(p));

        return values.Count > 0 ? JsonSerializer.Serialize(values) : null;
    }
}
