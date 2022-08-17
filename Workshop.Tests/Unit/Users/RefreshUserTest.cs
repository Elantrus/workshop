using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Users.Application.Features;
using Users.Contracts;
using Users.Core.Services;
using Users.Infrastructure.Data;

namespace Workshop.Tests.Unit.Users;

public class RefreshCustomerTest
{
    [Test]
    public async Task Test_UserRefreshShouldReturnTokenAndRefreshToken()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var tokenService = new TokenService("E)H@McQfTjWnZr4u7x!A%D*G-JaNdRgU") as ITokenService;
        var createUserHandler = new CreateCustomerApplicationHandler(dbContext);
        var authenticateUserHandler = new AuthenticateUserApplicationHandler(dbContext, tokenService);
        var refreshCustomerHandler = new RefreshUserApplicationHandler(dbContext, tokenService);
        var password = "str0ng@!PasS";
        var email = "teste@gmail.com";
        var command = new CreateCustomer.CreateCustomerCommand
        {
            Email = email,
            Name = "Lazaro Junior",
            Password = password
        };
        var authenticateCommand = new AuthenticateUser.AuthenticateUserCommand
        {
            Email = email,
            Password = password
        };
        var refreshCommand = new RefreshUser.RefreshUserCommand();
        await createUserHandler.Handle(command, CancellationToken.None);
        var authenticationResult = await authenticateUserHandler.Handle(authenticateCommand, CancellationToken.None);
        refreshCommand.RefreshToken = authenticationResult.RefreshToken;

        var refreshResult = await refreshCustomerHandler.Handle(refreshCommand, CancellationToken.None);

        Assert.NotNull(refreshResult);
        Assert.NotNull(refreshResult.Token);
        Assert.NotNull(refreshResult.RefreshToken);
    }
}