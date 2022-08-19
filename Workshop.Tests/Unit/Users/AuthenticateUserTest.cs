using System.Threading;
using System.Threading.Tasks;
using NUnit.Framework;
using Users.Application.Features;
using Users.Application.Services;
using Users.Core.Exceptions;
using Users.Core.Services;

namespace Workshop.Tests.Unit.Users;

public class AuthenticateUserTest
{
    [Test]
    public async Task Test_UserAuthenticationShouldReturnTokenAndRefreshToken()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var tokenService = new TokenService("E)H@McQfTjWnZr4u7x!A%D*G-JaNdRgU") as ITokenService;
        var createUserHandler = new CreateCustomer.Handler(dbContext);
        var authenticateUserHandler = new AuthenticateUser.Handler(dbContext, tokenService);
        var password = "str0ng@!PasS";
        var email = "teste@gmail.com";
        var command = new CreateCustomer.Command()
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
    
    [Test]
    public void Test_UserAuthenticationShouldThrowInvalidEmail()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var tokenService = new TokenService("E)H@McQfTjWnZr4u7x!A%D*G-JaNdRgU") as ITokenService;
        var authenticateUserHandler = new AuthenticateUser.Handler(dbContext, tokenService);
        var password = "str0ng@!PasS";
        var email = "teste@gmail.com";
        
        var authenticateCommand = new AuthenticateUser.Command
        {
            Email = email,
            Password = password
        };

        Assert.ThrowsAsync<InvalidEmailException>(async () =>
            await authenticateUserHandler.Handle(authenticateCommand, CancellationToken.None));
    }
    
    [Test]
    public async Task Test_UserAuthenticationShouldThrowInvalidPassword()
    {
        var dbContext = InMemoryDatabase.CreateUsersDb();

        var tokenService = new TokenService("E)H@McQfTjWnZr4u7x!A%D*G-JaNdRgU") as ITokenService;
        var createUserHandler = new CreateCustomer.Handler(dbContext);
        var authenticateUserHandler = new AuthenticateUser.Handler(dbContext, tokenService);
        var password = "str0ng@!PasS";
        var email = "teste@gmail.com";
        var command = new CreateCustomer.Command()
        {
            Email = email,
            Name = "Lazaro Junior",
            Password = password
        };
        
        var authenticateCommand = new AuthenticateUser.Command()
        {
            Email = email,
            Password = "anypassword"
        };
        await createUserHandler.Handle(command, CancellationToken.None);
        
        Assert.ThrowsAsync<InvalidPasswordException>(async () =>
            await authenticateUserHandler.Handle(authenticateCommand, CancellationToken.None));
    }
    
    
}