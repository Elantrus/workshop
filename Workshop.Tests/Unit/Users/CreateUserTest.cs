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

public class CreateUserTest
{
    [Test]
    public async Task Test_CreateUserShouldReturnIdGreaterThanZero()
    {
        var options = new DbContextOptionsBuilder<UsersDbContext>()
            .UseInMemoryDatabase(databaseName: "workshoptest.users")
            .Options;
        var dbContext = new UsersDbContext(options);
        var handler = new CreateUserApplicationHandler(dbContext);
        var command = new CreateUser.Command
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
    public async Task Test_CreateUserShouldReturnUserAlreadyExistsException()
    {
        var options = new DbContextOptionsBuilder<UsersDbContext>()
            .UseInMemoryDatabase(databaseName: "workshoptest.users")
            .Options;
        var dbContext = new UsersDbContext(options);
        var handler = new CreateUserApplicationHandler(dbContext);
        var command = new CreateUser.Command
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
    public async Task Test_UserAuthenticationShouldReturnTokenAndRefreshToken()
    {
        var options = new DbContextOptionsBuilder<UsersDbContext>()
            .UseInMemoryDatabase(databaseName: "workshoptest.users")
            .Options;
        var dbContext = new UsersDbContext(options);
        var createUserHandler = new CreateUserApplicationHandler(dbContext);
        var authenticateUserHandler = new AuthenticateCustomerApplicationHandler(dbContext);
        var password = "str0ng@!PasS";
        var email = "teste@gmail.com";
        var command = new CreateUser.Command
        {
            Email = email,
            Name = "Lazaro Junior",
            Password = password
        };
        
        var authenticateCommand = new AuthenticateUser.Command
        {
            Email = email,
            Password = password
        };
        await createUserHandler.Handle(command, CancellationToken.None);
        
        var authenticationResult = await authenticateUserHandler.Handle(authenticateCommand, CancellationToken.None);

        Assert.NotNull(authenticationResult);
        Assert.NotNull(authenticationResult.Token);
        Assert.NotNull(authenticationResult.RefreshToken);
    }
}