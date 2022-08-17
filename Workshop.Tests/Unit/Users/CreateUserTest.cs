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
    public async Task Test_CreateCustomerShouldReturnIdGreaterThanZero()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateCustomerApplicationHandler(dbContext);
        var command = new CreateCustomer.CreateCustomerCommand
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

        var handler = new CreateCustomerApplicationHandler(dbContext);
        var command = new CreateCustomer.CreateCustomerCommand
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
}