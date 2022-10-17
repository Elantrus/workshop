using System;
using Microsoft.EntityFrameworkCore;
using Repairing.Infrastructure.Data;
using Users.Infrastructure.Data;

namespace Workshop.Tests.Unit;

public class InMemoryDatabase
{
    public static UsersDbContext CreateUsersDb()
    {
        var options = new DbContextOptionsBuilder<UsersDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        var dbContext = new UsersDbContext(options);

        dbContext.Database.EnsureCreated();

        return dbContext;
    }
    
    public static RepairingDbContext CreateRepairingDb()
    {
        var options = new DbContextOptionsBuilder<RepairingDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        var dbContext = new RepairingDbContext(options);

        dbContext.Database.EnsureCreated();

        return dbContext;
    }
}