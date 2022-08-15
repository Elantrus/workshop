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

public class AuthenticateUserTest
{
    [Test]
    public async Task Test_UserAuthenticationShouldReturnTokenAndRefreshToken()
    {
        var options = new DbContextOptionsBuilder<UsersDbContext>()
            .UseInMemoryDatabase(Guid.NewGuid().ToString())
            .Options;
        var dbContext = new UsersDbContext(options);
        var tokenService = new TokenService("E)H@McQfTjWnZr4u7x!A%D*G-JaNdRgU") as ITokenService;
        var createUserHandler = new CreateCustomerApplicationHandler(dbContext);
        var authenticateUserHandler = new AuthenticateUserApplicationHandler(dbContext, tokenService);
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
        await createUserHandler.Handle(command, CancellationToken.None);
        
        var authenticationResult = await authenticateUserHandler.Handle(authenticateCommand, CancellationToken.None);

        Assert.NotNull(authenticationResult);
        Assert.NotNull(authenticationResult.Token);
        Assert.NotNull(authenticationResult.RefreshToken);
    }
    
    
}