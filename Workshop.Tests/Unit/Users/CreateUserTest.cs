using System;
using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Users.Application.Features;
using Users.Core.Exceptions;

namespace Workshop.Tests.Unit.Users;

public class CreateUserTest
{
    [Test]
    public async Task Test_CreateCustomerShouldReturnIdGreaterThanZero()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateCustomer.Handler(dbContext);
        var command = new CreateCustomer.Command
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
    public async Task Test_CreateCustomerShouldReturnUserAlreadyExistsException()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateCustomer.Handler(dbContext);
        var command = new CreateCustomer.Command
        {
            Email = "teste@gmail.com",
            Name = "Lazaro Junior",
            Password = "str0ng@!PasS"
        };
        await handler.Handle(command, CancellationToken.None);

        Exception? exception = null;
        try
        {
            await handler.Handle(command, CancellationToken.None);
        }
        catch (Exception? ex)
        {
            exception = ex;
        }
        
        Assert.NotNull(exception);
        Assert.IsInstanceOf(typeof(EmailAlreadyExistsException), exception);
    }
    
    
    [Test]
    public void Test_CreateCustomerShouldThrowInvalidEmail()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateCustomer.Handler(dbContext);
        var command = new CreateCustomer.Command
        {
            Email = "teste",
            Name = "Lazaro Junior",
            Password = "str0ng@!PasS"
        };
        
        Assert.ThrowsAsync<InvalidEmailException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public void Test_CreateCustomerShouldThrowPasswordTooWeak()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateCustomer.Handler(dbContext);
        var command = new CreateCustomer.Command
        {
            Email = "teste@gmail.com",
            Name = "Lazaro Junior",
            Password = "notstrongoass"
        };
        
        Assert.ThrowsAsync<PasswordIsTooWeakException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public void Test_CreateCustomerShouldThrowPasswordTooShort()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateCustomer.Handler(dbContext);
        var command = new CreateCustomer.Command
        {
            Email = "teste@gmail.com",
            Name = "Lazaro Junior",
            Password = "a"
        };
        
        Assert.ThrowsAsync<PasswordTooShortException>(async () => await handler.Handle(command, CancellationToken.None));
    }
    
    [Test]
    public void Test_CreateCustomerShouldThrowFullNameTooShort()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateCustomer.Handler(dbContext);
        var command = new CreateCustomer.Command()
        {
            Email = "teste@gmail.com",
            Name = "a",
            Password = "sSwe2#2L@"
        };
        
        Assert.ThrowsAsync<FullNameTooShortException>(async () => await handler.Handle(command, CancellationToken.None));
    }
}