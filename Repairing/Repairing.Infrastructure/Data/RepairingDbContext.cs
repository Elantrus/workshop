using Microsoft.EntityFrameworkCore;
using Repairing.Core.Entities;

namespace Repairing.Infrastructure.Data;

public class RepairingDbContext : DbContext
{
    public RepairingDbContext(DbContextOptions<RepairingDbContext> options) : base(options)
    {
    }
    
    public DbSet<Car> Cars { get; set; }
    public DbSet<Repair> Repairs { get; set; }
}