using Microsoft.EntityFrameworkCore;
using SchoolManagement.Domain.Entities;

namespace PreSchoolManagement.Infrastructure.Data;

public partial class ApplicationDbContext
{
    public DbSet<SchoolRegistration> SchoolRegistrations => Set<SchoolRegistration>();
    public DbSet<CommitteeMaster> CommitteeMasters => Set<CommitteeMaster>();
}
