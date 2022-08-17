using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Users.Application.Features;
using Users.Contracts;
using Users.Infrastructure.Data;

namespace Workshop.Tests.Unit.Users;

public class CreateAdminTest
{
    [Test]
    public async Task Test_CreateAdminShouldReturnUserId()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var handler = new CreateAdminApplicationHandler(dbContext);
        var command = new CreateAdmin.CreateAdminCommand
        {
            Email = "teste@gmail.com",
            Name = "Lazaro Junior",
            Password = "str0ng@!PasS"
        };
        
        var sendResult = await handler.Handle(command, CancellationToken.None);

        Assert.NotNull(sendResult);
        Assert.NotZero(sendResult.UserId);
    }
}