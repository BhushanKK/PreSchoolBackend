using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data.Configurations;

public class UserDetailsMasterConfiguration : IEntityTypeConfiguration<UserDetailsMaster>
{
    public void Configure(EntityTypeBuilder<UserDetailsMaster> entity)
    {
        entity.ToTable("UserDetailsMaster");
        entity.HasKey(e => e.UserId);
    }
}
