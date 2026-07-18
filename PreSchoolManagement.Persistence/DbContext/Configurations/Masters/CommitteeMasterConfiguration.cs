using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class CommitteeMasterConfiguration : IEntityTypeConfiguration<CommitteeMaster>
{
    public void Configure(EntityTypeBuilder<CommitteeMaster> entity)
    {
        entity.ToTable("CommitteeMaster");
        entity.HasKey(e => e.CommitteeId);
    }
}
