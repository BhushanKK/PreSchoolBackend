using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class CategoryMasterConfiguration : IEntityTypeConfiguration<CategoryMaster>
{
    public void Configure(EntityTypeBuilder<CategoryMaster> entity)
    {
        entity.ToTable("CategoryMaster");
        entity.HasKey(e => e.CategoryId);
    }
}
