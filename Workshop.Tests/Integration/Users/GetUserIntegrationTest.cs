using System.Threading.Tasks;
using NUnit.Framework;
using Users.Application.Features;

namespace Workshop.Tests.Integration.Users;

public class GetUserIntegrationTest
{
    [Test]
    public async Task Test_GetUserShouldReturnUserInfo()
    {
        var testServer = new IntegrationTestServer();
        var email = "spynrt@gmail.com";
        var password = "S4$roNg)9";
        await testServer.CreateCustomerAndLogin(email, password);

        var result = await testServer.Get<GetUser.Result>("/api/user");
        
        Assert.NotNull(result);
        Assert.NotNull(result.Email);
        Assert.NotNull(result.FullName);
    }
}