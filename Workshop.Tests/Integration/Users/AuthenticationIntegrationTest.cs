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
        var authenticationCommand = new AuthenticateUser.AuthenticateUserCommand
        {
            Email = email,
            Password = password
        };

        var result = await testServer.Post<AuthenticateUser.AuthenticateUserResult>("/api/authentication", authenticationCommand);
        var deserializedResponse = result.DeserializedResponse;
        
        Assert.NotNull(deserializedResponse);
        Assert.NotNull(deserializedResponse?.Token);
        Assert.NotNull(deserializedResponse?.RefreshToken);
        Assert.IsNotEmpty(deserializedResponse?.Token);
        Assert.IsNotEmpty(deserializedResponse?.RefreshToken);
    }
    
    [Test]
    public async Task Test_RefreshShouldReturnToken()
    {   
        var testServer = new IntegrationTestServer();
        var refreshToken = await testServer.LoginAsSystem();

        var refreshCommand = new RefreshUser.RefreshUserCommand
        {
            RefreshToken = refreshToken
        };
        var result = await testServer.Post<RefreshUser.RefreshUserResult>("/api/authentication/refresh", refreshCommand);
        var deserializedResponse = result.DeserializedResponse;
        
        Assert.NotNull(deserializedResponse);
        Assert.NotNull(deserializedResponse?.Token);
        Assert.NotNull(deserializedResponse?.RefreshToken);
        Assert.IsNotEmpty(deserializedResponse?.Token);
        Assert.IsNotEmpty(deserializedResponse?.RefreshToken);
    }
}