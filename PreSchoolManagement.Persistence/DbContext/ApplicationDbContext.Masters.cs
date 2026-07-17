using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data;

public partial class ApplicationDbContext
{
    public DbSet<ReligionMaster> ReligionMasters => Set<ReligionMaster>();
    public DbSet<CategoryMaster> CategoryMasters => Set<CategoryMaster>();
    public DbSet<CasteMaster> CasteMasters => Set<CasteMaster>();
    public DbSet<StandardMaster> StandardMasters => Set<StandardMaster>();
    public DbSet<DivisionMaster> DivisionMasters => Set<DivisionMaster>();
    public DbSet<HolidayMaster> HolidayMasters => Set<HolidayMaster>();
    public DbSet<AcademicYearMaster> AcademicYearMasters => Set<AcademicYearMaster>();
    public DbSet<FinancialYearMaster> FinancialYearMasters => Set<FinancialYearMaster>();
    public DbSet<SectionMaster> SectionMasters => Set<SectionMaster>();
    public DbSet<DistrictMaster> DistrictMasters => Set<DistrictMaster>();
    public DbSet<StateMaster> StateMasters => Set<StateMaster>();
    public DbSet<EmployeeTypeMaster> EmployeeTypeMasters => Set<EmployeeTypeMaster>();
    public DbSet<DesignationMaster> DesignationMasters => Set<DesignationMaster>();
    public DbSet<BoardMaster> BoardMasters => Set<BoardMaster>();
    public DbSet<RoleMaster> RoleMasters => Set<RoleMaster>();
}
