using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class MenuMasterConfiguration : IEntityTypeConfiguration<MenuMaster>
{
    public void Configure(EntityTypeBuilder<MenuMaster> entity)
    {
        entity.ToTable("MenuMaster");
        entity.HasKey(e => e.MenuId);
    }
}
