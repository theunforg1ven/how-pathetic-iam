using HPIAM.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace HPIAM.Infrastructure.Data;
public  class DataContext(DbContextOptions options) : DbContext(options) // primary constructor
{
    public DbSet<AppUser> Users { get; set; } // Db sql table name

    public DbSet<Member> Members { get; set; }

    public DbSet<Photo> Photos { get; set; }
}
