using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class DesignationMasterConfiguration : IEntityTypeConfiguration<DesignationMaster>
{
    public void Configure(EntityTypeBuilder<DesignationMaster> entity)
    {
        entity.ToTable("DesignationMaster");
        entity.HasKey(e => e.DesignationId);
    }
}
