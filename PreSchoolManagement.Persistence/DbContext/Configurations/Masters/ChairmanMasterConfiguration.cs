using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

public class ChairmanMasterConfiguration : IEntityTypeConfiguration<ChairmanMaster>
{
    public void Configure(EntityTypeBuilder<ChairmanMaster> entity)
    {
        entity.ToTable("ChairmanMaster");
        entity.HasKey(x => x.ChairmanId);
    }
}