using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class DivisionMasterConfiguration : IEntityTypeConfiguration<DivisionMaster>
{
    public void Configure(EntityTypeBuilder<DivisionMaster> entity)
    {
        entity.ToTable("DivisionMaster");
        entity.HasKey(e => e.DivisionId);
    }
}
