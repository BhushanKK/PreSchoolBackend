using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class StandardMasterConfiguration : IEntityTypeConfiguration<StandardMaster>
{
    public void Configure(EntityTypeBuilder<StandardMaster> entity)
    {
        entity.ToTable("StandardMaster");
        entity.HasKey(e => e.StandardId);
    }
}
