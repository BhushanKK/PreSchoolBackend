using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class RoleMenuPermissionConfiguration : IEntityTypeConfiguration<RoleMenuPermission>
{
    public void Configure(EntityTypeBuilder<RoleMenuPermission> entity)
    {
        entity.ToTable("RoleMenuPermission");
        entity.HasKey(e => e.RoleMenuPermissionId);
    }
}
