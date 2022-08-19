using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Users.Application.Features;
using Users.Core.Exceptions;

namespace Workshop.Tests.Unit.Users;

public class CreateAdminTest
{
    [Test]
    public async Task Test_CreateAdminShouldReturnUserId()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateAdmin.Handler(dbContext);
        var command = new CreateAdmin.Command()
        {
            Email = "teste@gmail.com",
            Name = "Lazaro Junior",
            Password = "str0ng@!PasS"
        };
        
        var sendResult = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(sendResult);
        Assert.NotZero(sendResult.UserId);
    }
    
    [Test]
    public void Test_CreateAdminShouldThrowInvalidEmail()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateAdmin.Handler(dbContext);
        var command = new CreateAdmin.Command
        {
            Email = "teste",
            Name = "Lazaro Junior",
            Password = "str0ng@!PasS"
        };
        
        Assert.ThrowsAsync<InvalidEmailException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public void Test_CreateAdminShouldThrowPasswordTooWeak()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateAdmin.Handler(dbContext);
        var command = new CreateAdmin.Command
        {
            Email = "teste@gmail.com",
            Name = "Lazaro Junior",
            Password = "notstrongoass"
        };
        
        Assert.ThrowsAsync<PasswordIsTooWeakException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public void Test_CreateAdminShouldThrowPasswordTooShort()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateAdmin.Handler(dbContext);
        var command = new CreateAdmin.Command
        {
            Email = "teste@gmail.com",
            Name = "Lazaro Junior",
            Password = "a"
        };
        
        Assert.ThrowsAsync<PasswordTooShortException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public void Test_CreateAdminShouldThrowFullNameTooShort()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateAdmin.Handler(dbContext);
        var command = new CreateAdmin.Command
        {
            Email = "teste@gmail.com",
            Name = "a",
            Password = "sSwe2#2L@"
        };
        
        Assert.ThrowsAsync<FullNameTooShortException>(async () => await handler.Handle(command, CancellationToken.None));
    }
}