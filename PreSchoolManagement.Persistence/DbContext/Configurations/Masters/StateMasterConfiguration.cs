using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class StateMasterConfiguration : IEntityTypeConfiguration<StateMaster>
{
    public void Configure(EntityTypeBuilder<StateMaster> entity)
    {
        entity.ToTable("StateMaster");
        entity.HasKey(e => e.StateId);
    }
}
