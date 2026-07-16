using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class BoardMasterConfiguration : IEntityTypeConfiguration<BoardMaster>
{
    public void Configure(EntityTypeBuilder<BoardMaster> entity)
    {
        entity.ToTable("BoardMaster");
        entity.HasKey(e => e.BoardId);
    }
}
