using System.Threading.Tasks;
using NUnit.Framework;
using Users.Application.Features;

namespace Workshop.Tests.Integration.Users;

public class AuthenticateUserIntegrationTest
{
    [Test]
    public async Task Test_AuthenticationShouldReturnTokenAndRefreshToken()
    {
        var testServer = new IntegrationTestServer();
        var email = "spynrt@gmail.com";
        var password = "S4$roNg)9";
        var createdUser = await testServer.CreateCustomer(email, password);
        var authenticationCommand = new AuthenticateUser.Command
        {
            Email = email,
            Password = password
        };

        var result = await testServer.Post<AuthenticateUser.Result>("/api/authentication", authenticationCommand);
        
        Assert.NotNull(result);
        Assert.NotNull(result.Token);
        Assert.NotNull(result.RefreshToken);
        Assert.IsNotEmpty(result.Token);
        Assert.IsNotEmpty(result.RefreshToken);
    }
}