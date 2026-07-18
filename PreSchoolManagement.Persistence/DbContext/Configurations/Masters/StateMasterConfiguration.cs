using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using PreSchoolManagement.Domain.Utils;
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

public class StateTranslationConfiguration : IEntityTypeConfiguration<StateTranslation>
{
    public void Configure(EntityTypeBuilder<StateTranslation> entity)
    {
        entity.ToTable("StateTranslation");

        entity.HasKey(x => x.StateTranslationId);

        entity.Property(x => x.LanguageCode)
            .HasMaxLength(5)
            .IsRequired();
        
        entity.Property(x => x.StateName)
            .HasMaxLength(100)
            .IsRequired();
        
        entity.HasIndex(x => new
        {
            x.StateId,
            x.LanguageCode
        }).IsUnique();
        
        entity.HasOne(x => x.State)
            .WithMany(x => x.Translations)
            .HasForeignKey(x => x.StateId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
