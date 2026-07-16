using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class SchoolRegistrationConfiguration : IEntityTypeConfiguration<SchoolRegistration>
{
    public void Configure(EntityTypeBuilder<SchoolRegistration> entity)
    {
        entity.ToTable("SchoolRegistration");
        entity.HasKey(e => e.SchoolRegistrationId);
    }
}
