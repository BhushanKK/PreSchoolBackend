using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data;

public partial class ApplicationDbContext
{
    public DbSet<MenuMaster> MenuMasters => Set<MenuMaster>();
    public DbSet<RoleMenuPermission> RoleMenuPermissions => Set<RoleMenuPermission>();
    public DbSet<MenuTranslation> MenuTranslations => Set<MenuTranslation>();
}
