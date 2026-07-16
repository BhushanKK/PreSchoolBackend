using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class CasteMasterConfiguration : IEntityTypeConfiguration<CasteMaster>
{
    public void Configure(EntityTypeBuilder<CasteMaster> entity)
    {
        entity.ToTable("CasteMaster");
        entity.HasKey(e => e.CasteID);
    }
}
