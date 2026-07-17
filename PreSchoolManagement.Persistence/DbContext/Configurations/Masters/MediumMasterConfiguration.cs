using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class MediumMasterConfiguration : IEntityTypeConfiguration<MediumMaster>
{
    public void Configure(EntityTypeBuilder<MediumMaster> entity)
    {
        entity.ToTable("MediumMaster");
        entity.HasKey(e => e.MediumId);
    }
}
