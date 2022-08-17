using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Users.Application.Features;
using Users.Contracts;
using Users.Core.Exceptions;
using Users.Infrastructure.Data;

namespace Workshop.Tests.Unit.Users;

public class CreateRoleTest
{
    [Test]
    public async Task Test_ShouldCreateRole()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();
        var handler = new CreateRoleApplicationHandler(dbContext);
        var command = new CreateRole.CreateRoleCommand
        {
            Name = "toplaner",
        };
        
        var sendResult = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(sendResult);
    }
    
    [Test]
    public async Task Test_CreateRoleShouldFailBecauseOfExistingRole()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateRoleApplicationHandler(dbContext);
        var command = new CreateRole.CreateRoleCommand
        {
            Name = "admin",
        };

        Exception? givenException = null; 
        try
        {
            await handler.Handle(command, CancellationToken.None);
        }
        catch (Exception? ex)
        {
            givenException = ex;
        }
        
        Assert.NotNull(givenException);
        Assert.IsInstanceOf<RoleAlreadyExistException>(givenException);
    }
}