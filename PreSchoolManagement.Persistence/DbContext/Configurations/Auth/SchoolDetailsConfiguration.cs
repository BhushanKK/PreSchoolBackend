using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class SchoolDetailsConfiguration : IEntityTypeConfiguration<SchoolDetailsMaster>
{
    public void Configure(EntityTypeBuilder<SchoolDetailsMaster>entity)
    {
        entity.ToTable("SchoolDetailsMaster");
        entity.HasKey(e => e.SchoolDetailsId);
    }
}