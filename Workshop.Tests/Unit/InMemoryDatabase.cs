using System;
using Microsoft.EntityFrameworkCore;
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
}