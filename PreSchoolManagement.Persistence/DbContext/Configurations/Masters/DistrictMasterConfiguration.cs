using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class DistrictMasterConfiguration : IEntityTypeConfiguration<DistrictMaster>
{
    public void Configure(EntityTypeBuilder<DistrictMaster> entity)
    {
        entity.ToTable("DistrictMaster");
        entity.HasKey(e => e.DistrictId);
    }
}
