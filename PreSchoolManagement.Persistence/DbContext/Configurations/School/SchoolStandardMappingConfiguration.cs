using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class SchoolStandardMappingConfiguration : IEntityTypeConfiguration<SchoolStandardMapping>
{
    public void Configure(EntityTypeBuilder<SchoolStandardMapping> entity)
    {
        entity.ToTable("SchoolStandardMapping");
        entity.HasKey(x => x.SchoolStandardMappingId);
    }
}