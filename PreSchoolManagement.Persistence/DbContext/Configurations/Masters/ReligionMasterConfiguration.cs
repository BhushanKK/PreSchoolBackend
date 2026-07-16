using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class ReligionMasterConfiguration : IEntityTypeConfiguration<ReligionMaster>
{
    public void Configure(EntityTypeBuilder<ReligionMaster> entity)
    {
        entity.ToTable("ReligionMaster");
        entity.HasKey(e => e.ReligionId);
    }
}
