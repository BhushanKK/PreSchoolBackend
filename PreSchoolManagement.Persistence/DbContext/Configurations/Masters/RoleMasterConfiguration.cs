using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class RoleMasterConfiguration : IEntityTypeConfiguration<RoleMaster>
{
    public void Configure(EntityTypeBuilder<RoleMaster> entity)
    {
        entity.ToTable("RoleMaster");
        entity.HasKey(e => e.RoleId);
    }
}
    
public class RoleTranslationConfiguration : IEntityTypeConfiguration<RoleTranslation>
{
    public void Configure(EntityTypeBuilder<RoleTranslation> entity)
    {
        entity.ToTable("RoleTranslation");

        entity.HasKey(x => x.RoleTranslationId);

        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();

        entity.Property(x => x.RoleName)
            .HasMaxLength(100)
            .IsRequired();

        entity.HasIndex(x => new
        {
            x.RoleId,
            x.LanguageCode
        }).IsUnique();

        entity.HasOne(x => x.Role)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
