using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class RoleMasterConfiguration : IEntityTypeConfiguration<RoleMaster>
{
    public void Configure(EntityTypeBuilder<RoleMaster> entity)
    {
        entity.ToTable("RoleMaster");
        entity.HasKey(e => e.RoleId);
    }
}
