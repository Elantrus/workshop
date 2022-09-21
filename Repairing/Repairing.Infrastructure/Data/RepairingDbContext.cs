using Microsoft.EntityFrameworkCore;

namespace Repairing.Infrastructure.Data;

public class RepairingDbContext : DbContext
{
    public RepairingDbContext(DbContextOptions<RepairingDbContext> options) : base(options)
    {
    }
    
    
}