using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class AcademicYearMasterConfiguration : IEntityTypeConfiguration<AcademicYearMaster>
{
    public void Configure(EntityTypeBuilder<AcademicYearMaster> entity)
    {
        entity.ToTable("AcademicYearMaster");
        entity.HasKey(e => e.AcademicYearId);
    }
}
