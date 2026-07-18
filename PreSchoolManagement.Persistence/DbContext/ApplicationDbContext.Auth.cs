using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data;

public partial class ApplicationDbContext
{
    public DbSet<UserDetailsMaster> UserDetailsMasters => Set<UserDetailsMaster>();
    public DbSet<RefreshToken> RefreshTokens { get; set; }
    public DbSet<PasswordResetToken> PasswordResetTokens => Set<PasswordResetToken>();
}
