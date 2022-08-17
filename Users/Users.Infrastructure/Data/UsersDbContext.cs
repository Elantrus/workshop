using Microsoft.EntityFrameworkCore;
using Users.Core.Entities;
using Users.Infrastructure.Seeds;

namespace Users.Infrastructure.Data;

public class UsersDbContext : DbContext
{
    public UsersDbContext(DbContextOptions<UsersDbContext> options) : base(options)
    {

    }
    public DbSet<User> Users { get; set; }
    public DbSet<Customer> Customers { get; set; }
    public DbSet<Administrator> Administrators { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Role>().HasRoles();
        base.OnModelCreating(modelBuilder);
    }
}