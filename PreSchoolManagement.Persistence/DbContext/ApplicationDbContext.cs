using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data;

public partial class ApplicationDbContext : DbContext
{
    public DbSet<ReligionMaster> ReligionMasters => Set<ReligionMaster>();
    public DbSet<CategoryMaster> CategoryMasters => Set<CategoryMaster>();
    public DbSet<CasteMaster> CasteMasters => Set<CasteMaster>();
    public DbSet<SchoolRegistration> SchoolRegistrations => Set<SchoolRegistration>();
    public DbSet<StandardMaster> StandardMasters => Set<StandardMaster>();
    public DbSet<DivisionMaster> DivisionMasters => Set<DivisionMaster>();
    public DbSet<RoleMaster> RoleMasters => Set<RoleMaster>();
    public DbSet<HolidayMaster> HolidayMasters => Set<HolidayMaster>();
    public DbSet<AcademicYearMaster> AcademicYearMasters => Set<AcademicYearMaster>();
    public DbSet<FinancialYearMaster> FinancialYearMasters => Set<FinancialYearMaster>();
    public DbSet<UserDetailsMaster> UserDetailsMasters => Set<UserDetailsMaster>();
    public DbSet<MenuMaster> MenuMasters => Set<MenuMaster>();
    public DbSet<SectionMaster> SectionMasters => Set<SectionMaster>();
    public DbSet<RoleMenuPermission> RoleMenuPermissions => Set<RoleMenuPermission>();
    public DbSet<DistrictMaster> DistrictMasters => Set<DistrictMaster>();
    public DbSet<StateMaster> StateMasters => Set<StateMaster>();
    public DbSet<EmployeeTypeMaster> EmployeeTypeMasters => Set<EmployeeTypeMaster>();
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<DesignationMaster> DesignationMasters => Set<DesignationMaster>();
    public DbSet<CommitteeMaster> CommitteeMasters => Set<CommitteeMaster>();
    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();
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

        modelBuilder.Entity<SchoolRegistration>(entity =>
        {
            entity.ToTable("SchoolRegistration");
            entity.HasKey(e => e.SchoolRegistrationId);
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

        modelBuilder.Entity<RefreshToken>(entity =>
        {
            entity.ToTable("RefreshToken");
            entity.HasKey(e => e.RefreshTokenId);
        });

        modelBuilder.Entity<SectionMaster>(entity =>
        {
            entity.ToTable("SectionMaster");
            entity.HasKey(e => e.SectionId);
        });

        modelBuilder.Entity<AuditLog>(entity =>
        {
            entity.ToTable("AuditLog");
            entity.HasKey(e => e.Id);
        });

        modelBuilder.Entity<MenuMaster>(entity =>
        {
            entity.ToTable("MenuMaster");
            entity.HasKey(e => e.MenuId);
        });

        modelBuilder.Entity<RoleMenuPermission>(entity =>
        {
            entity.ToTable("RoleMenuPermission");
            entity.HasKey(e => e.RoleMenuPermissionId);
        });

        modelBuilder.Entity<DistrictMaster>(entity =>
        {
            entity.ToTable("DistrictMaster");
            entity.HasKey(e => e.DistrictId);
        });

        modelBuilder.Entity<StateMaster>(entity =>
        {
            entity.ToTable("StateMaster");
            entity.HasKey(e => e.StateId);
        });

        modelBuilder.Entity<EmployeeTypeMaster>(entity =>
        {
            entity.ToTable("EmployeeTypeMaster");
            entity.HasKey(e => e.EmployeeTypeId);
        });

        modelBuilder.Entity<DesignationMaster>(entity =>
        {
            entity.ToTable("DesignationMaster");
            entity.HasKey(e => e.DesignationId);
        });

        modelBuilder.Entity<CommitteeMaster>(entity =>
        {
            entity.ToTable("CommitteeMaster");
            entity.HasKey(e => e.CommitteeId);
        });
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<PasswordResetToken>(entity =>
        {
            entity.ToTable("PasswordResetToken");

            entity.HasKey(x => x.PasswordResetTokenId);

            entity.HasOne(x => x.User)
                .WithMany(x => x.PasswordResetTokens)
                .HasForeignKey(x => x.UserId)
                .HasPrincipalKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        });
    }
}
