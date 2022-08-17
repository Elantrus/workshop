using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Users.Application.Features;
using Users.Contracts;
using Users.Core.Exceptions;

namespace Workshop.Tests.Unit.Users;

public class AssignAdministratorRoleTest
{
    [Test]
    public async Task Test_UserRoleShouldAssign()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();
        
        var createAdminHandler = new CreateAdminApplicationHandler(dbContext);
        var createAdminCommand = new CreateAdmin.CreateAdminCommand
        {
            Email = "teste@gmail.com",
            Name = "Lazaro Junior",
            Password = "str0ng@!PasS"
        };
        
        var createAdminResult = await createAdminHandler.Handle(createAdminCommand, CancellationToken.None);
        
        var handler = new AssignAdministratorRoleHandler(dbContext);
        var command = new AssignAdministratorRole.AssignAdministratorRoleCommand
        {
           RoleId = 1,
           UserId = createAdminResult.UserId
        };
        
        var sendResult = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(sendResult);
    }
    
    [Test]
    public async Task Test_AssignShouldThrowRoleNotFoundException()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();
       
        var handler = new AssignAdministratorRoleHandler(dbContext);
        var command = new AssignAdministratorRole.AssignAdministratorRoleCommand
        {
            RoleId = 999
        };
        Exception? assignRoleException = null;

        try
        {
            await handler.Handle(command, CancellationToken.None);
        }
        catch (Exception ex)
        {
            assignRoleException = ex;
        }

        Assert.NotNull(assignRoleException);
        Assert.IsInstanceOf<RoleNotFoundException>(assignRoleException);
    }
    
    [Test]
    public async Task Test_AssignShouldThrowUserNotFoundException()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();
       
        var handler = new AssignAdministratorRoleHandler(dbContext);
        var command = new AssignAdministratorRole.AssignAdministratorRoleCommand
        {
            RoleId = 1,
            UserId = long.MaxValue
        };
        Exception? assignRoleException = null;

        try
        {
            await handler.Handle(command, CancellationToken.None);
        }
        catch (Exception ex)
        {
            assignRoleException = ex;
        }

        Assert.NotNull(assignRoleException);
        Assert.IsInstanceOf<UserNotFoundException>(assignRoleException);
    }
}