using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class SectionMasterConfiguration : IEntityTypeConfiguration<SectionMaster>
{
    public void Configure(EntityTypeBuilder<SectionMaster> entity)
    {
        entity.ToTable("SectionMaster");
        entity.HasKey(e => e.SectionId);
    }
}
