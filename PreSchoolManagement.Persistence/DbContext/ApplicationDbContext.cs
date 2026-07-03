using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace SchoolAdmission.Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public DbSet<ReligionMaster> ReligionMasters => Set<ReligionMaster>();
    public DbSet<CategoryMaster> CategoryMasters => Set<CategoryMaster>();
    public DbSet<CasteMaster> CasteMasters => Set<CasteMaster>();
    public DbSet<CommiteeMaster> CommiteeMasters => Set<CommiteeMaster>();
    public DbSet<SchoolDetailsMaster> SchoolDetailsMasters => Set<SchoolDetailsMaster>();
    public DbSet<StandardMaster> StandardMasters => Set<StandardMaster>();
    public DbSet<DivisionMaster> DivisionMasters => Set<DivisionMaster>();
    public DbSet<RoleMaster> Roles => Set<RoleMaster>();
    public DbSet<HolidayMaster> HolidayMasters => Set<HolidayMaster>();
    public DbSet<AcademicYearMaster> AcademicYearMasters => Set<AcademicYearMaster>();
   public DbSet<FinancialYearMaster> FinancialYearMasters => Set<FinancialYearMaster>(); 
    public DbSet<UserDetailsMaster> UserDetailsMasters => Set<UserDetailsMaster>();

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

        modelBuilder.Entity<AcademicYearMaster>(entity =>
        {
            entity.ToTable("AcademicYearMaster");
            entity.HasKey(e => e.AcademicYearId);
        });

        modelBuilder.Entity<FinancialYearMaster>(entity =>
        {
            entity.ToTable("FinancialYearMaster");
            entity.HasKey(e => e.FinancialYearId);
        });

        modelBuilder.Entity<UserDetailsMaster>(entity =>
        {
            entity.ToTable("UserDetailsMaster");
            entity.HasKey(e => e.UserId);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("AuditLog");
            entity.HasKey(e => e.Id);
        });

        base.OnModelCreating(modelBuilder);
    }
}
