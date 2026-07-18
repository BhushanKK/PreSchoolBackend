using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data;

public partial class ApplicationDbContext
{
    public DbSet<ReligionMaster> ReligionMasters => Set<ReligionMaster>();
    public DbSet<CategoryMaster> CategoryMasters => Set<CategoryMaster>();
    public DbSet<CategoryTranslation> CategoryTranslations => Set<CategoryTranslation>();
    public DbSet<CasteMaster> CasteMasters => Set<CasteMaster>();
    public DbSet<CasteTranslation> CasteTranslations => Set<CasteTranslation>();
    public DbSet<StandardMaster> StandardMasters => Set<StandardMaster>();
    public DbSet<DivisionMaster> DivisionMasters => Set<DivisionMaster>();
    public DbSet<DivisionTranslation> DivisionTranslations => Set<DivisionTranslation>();
    public DbSet<HolidayMaster> HolidayMasters => Set<HolidayMaster>();
    public DbSet<AcademicYearMaster> AcademicYearMasters => Set<AcademicYearMaster>();
    public DbSet<AcademicYearTranslation> AcademicYearTranslations => Set<AcademicYearTranslation>();
    public DbSet<FinancialYearMaster> FinancialYearMasters => Set<FinancialYearMaster>();
    public DbSet<SectionMaster> SectionMasters => Set<SectionMaster>();
    public DbSet<SectionTranslation> SectionTranslations => Set<SectionTranslation>();
    public DbSet<DistrictMaster> DistrictMasters => Set<DistrictMaster>();
    public DbSet<DistrictTranslation> DistrictTranslations => Set<DistrictTranslation>();
    public DbSet<StateMaster> StateMasters => Set<StateMaster>();
    public DbSet<EmployeeTypeMaster> EmployeeTypeMasters => Set<EmployeeTypeMaster>();
    public DbSet<DesignationMaster> DesignationMasters => Set<DesignationMaster>();
    public DbSet<DesignationTranslation> DesignationTranslations => Set<DesignationTranslation>();
    public DbSet<BoardMaster> BoardMasters => Set<BoardMaster>();
    public DbSet<BoardTranslation> BoardTranslations => Set<BoardTranslation>();
    public DbSet<RoleMaster> RoleMasters => Set<RoleMaster>();
    public DbSet<RoleTranslation> RoleTranslations => Set<RoleTranslation>();
    public DbSet<MediumMaster> MediumMasters => Set<MediumMaster>();
    public DbSet<StateTranslation> StateTranslations => Set<StateTranslation>();
}
