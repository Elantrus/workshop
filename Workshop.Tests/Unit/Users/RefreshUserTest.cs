using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using NUnit.Framework;
using Users.Application.Features;
using Users.Application.Services;
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
        var createUserHandler = new CreateCustomer.Handler(dbContext);
        var authenticateUserHandler = new AuthenticateUser.Handler(dbContext, tokenService);
        var refreshCustomerHandler = new RefreshUser.Handler(dbContext, tokenService);
        var password = "str0ng@!PasS";
        var email = "teste@gmail.com";
        var command = new CreateCustomer.Command
        {
            Email = email,
            Name = "Lazaro Junior",
            Password = password
        };
        var authenticateCommand = new AuthenticateUser.Command()
        {
            Email = email,
            Password = password
        };
        var refreshCommand = new RefreshUser.Command();
        await createUserHandler.Handle(command, CancellationToken.None);
        var authenticationResult = await authenticateUserHandler.Handle(authenticateCommand, CancellationToken.None);
        refreshCommand.RefreshToken = authenticationResult.RefreshToken;

        var refreshResult = await refreshCustomerHandler.Handle(refreshCommand, CancellationToken.None);

        Assert.NotNull(refreshResult);
        Assert.NotNull(refreshResult.Token);
        Assert.NotNull(refreshResult.RefreshToken);
    }
}