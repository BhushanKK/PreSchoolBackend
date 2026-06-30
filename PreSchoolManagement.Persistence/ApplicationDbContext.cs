using System.Text.Json;
using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    private readonly AuditContext _auditContext;

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, AuditContext auditContext)
        : base(options)
    {
        _auditContext = auditContext;
    }
    public DbSet<ReligionMaster> ReligionMasters => Set<ReligionMaster>();
    public DbSet<CategoryMaster> CategoryMasters => Set<CategoryMaster>();
    public DbSet<CasteMaster> CasteMasters => Set<CasteMaster>();
    public DbSet<CommiteeMaster> CommiteeMasters => Set<CommiteeMaster>();
    public DbSet<SchoolDetailsMaster> SchoolDetailsMasters => Set<SchoolDetailsMaster>();
    public DbSet<StandardMaster> StandardMasters => Set<StandardMaster>();
    public DbSet<DivisionMaster> DivisionMasters => Set<DivisionMaster>();
    public DbSet<RoleMaster> Roles => Set<RoleMaster>();
    public DbSet<HolidayMaster> HolidayMasters => Set<HolidayMaster>();
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<ReligionMaster>(entity =>
        {
            entity.ToTable("ReligionMaster");
            entity.HasKey(e => e.ReligionId);
        });

        modelBuilder.Entity<CasteMaster>(entity =>
        {
            entity.ToTable("CasteMaster");
            entity.HasKey(e => e.CasteID);
        });

        modelBuilder.Entity<CategoryMaster>(entity =>
        {
            entity.ToTable("CategoryMaster");
            entity.HasKey(e => e.CategoryId);
        });

        modelBuilder.Entity<CommiteeMaster>(entity =>
        {
            entity.ToTable("CommiteeMaster");
            entity.HasKey(e => e.CommitteeId);
        });
        
        modelBuilder.Entity<SchoolDetailsMaster>(entity =>
        {
            entity.ToTable("SchoolDetailsMaster");
            entity.HasKey(e => e.SchoolId);
        });
        
        modelBuilder.Entity<StandardMaster>(entity =>
        {
            entity.ToTable("StandardMaster");
            entity.HasKey(e => e.StandardId);
        });
        
        modelBuilder.Entity<DivisionMaster>(entity =>
        {
            entity.ToTable("DivisionMaster");
            entity.HasKey(e => e.DivisionId);
        });
        
        modelBuilder.Entity<RoleMaster>(entity =>
        {
            entity.ToTable("RoleMaster");
            entity.HasKey(e => e.RoleId);
        });
        
        modelBuilder.Entity<HolidayMaster>(entity =>
        {
            entity.ToTable("HolidayMaster");
            entity.HasKey(e => e.HolidayId);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("AuditLog");
            entity.HasKey(e => e.Id);
        });

        base.OnModelCreating(modelBuilder);
    }
}
