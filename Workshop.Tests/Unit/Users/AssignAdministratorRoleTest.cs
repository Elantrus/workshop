using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Users.Application.Features;
using Users.Core.Exceptions;

namespace Workshop.Tests.Unit.Users;

public class AssignAdministratorRoleTest
{
    [Test]
    public async Task Test_UserRoleShouldAssign()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();
        
        var createAdminHandler = new CreateAdmin.Handler(dbContext);
        var createAdminCommand = new CreateAdmin.Command
        {
            Email = "teste@gmail.com",
            Name = "Lazaro Junior",
            Password = "str0ng@!PasS"
        };
        
        var createAdminResult = await createAdminHandler.Handle(createAdminCommand, CancellationToken.None);
        
        var handler = new AssignAdministratorRole.Handler(dbContext);
        var command = new AssignAdministratorRole.Command
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
       
        var handler = new AssignAdministratorRole.Handler(dbContext);
        var command = new AssignAdministratorRole.Command()
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
       
        var handler = new AssignAdministratorRole.Handler(dbContext);
        var command = new AssignAdministratorRole.Command()
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