using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Users.Application.Features;
using Users.Core.Exceptions;

namespace Workshop.Tests.Unit.Users;

public class CreateRoleTest
{
    [Test]
    public async Task Test_ShouldCreateRole()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();
        var handler = new CreateRole.Handler(dbContext);
        var command = new CreateRole.Command
        {
            Name = "toplaner",
        };
        
        var sendResult = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(sendResult);
    }
    
    [Test]
    public void Test_CreateRoleShouldThrowRoleAlreadyExist()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateRole.Handler(dbContext);
        var command = new CreateRole.Command
        {
            Name = "admin",
        };

        Assert.ThrowsAsync<RoleAlreadyExistException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public void Test_CreateRoleShouldThrowRoleNameTooShort()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateRole.Handler(dbContext);
        var command = new CreateRole.Command()
        {
            Name = "",
        };

        Assert.ThrowsAsync<RoleNameTooShortException>(async () =>
            await handler.Handle(command, CancellationToken.None));
    }
}